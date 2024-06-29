using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class CollectableData
{
    public List<Position> possiblePositions;
}

public class CollectableManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject gameEnderPrefab;

    public int bulletCount = 3;
    public int gameEnderCount = 3;

    private CollectableData collectableData;
    private HashSet<int> usedIndices = new HashSet<int>();

    void Start()
    {
        LoadCollectables();
    }

    void LoadCollectables()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "collectables.json");
#if UNITY_EDITOR
        // Load directly from StreamingAssets folder in Unity Editor
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            collectableData = JsonUtility.FromJson<CollectableData>(dataAsJson);
            Debug.Log("Collectables loaded successfully.");
            SpawnCollectables();
        }
        else
        {
            Debug.LogError("Cannot find collectables.json file.");
        }
#else
        // Load via UnityWebRequest on device
        StartCoroutine(LoadCollectablesFromDevice(filePath));
#endif
    }

    IEnumerator LoadCollectablesFromDevice(string filePath)
    {
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load collectables.json: " + request.error);
        }
        else
        {
            string dataAsJson = request.downloadHandler.text;
            collectableData = JsonUtility.FromJson<CollectableData>(dataAsJson);
            Debug.Log("Collectables loaded successfully.");
            SpawnCollectables();
        }
    }

    public void SpawnCollectables()
    {
        if (collectableData == null || collectableData.possiblePositions == null || collectableData.possiblePositions.Count == 0)
        {
            Debug.LogError("No collectable data found or possible positions list is empty.");
            return;
        }

        // Shuffle the list of possible positions
        List<Position> shuffledPositions = new List<Position>(collectableData.possiblePositions);
        ShuffleList(shuffledPositions);

        // Spawn bullets
        for (int i = 0; i < bulletCount; i++)
        {
            Position pos = GetUniquePosition(shuffledPositions);
            if (pos != null)
            {
                Debug.Log("Spawning bullet at: " + pos.x + ", " + pos.y + ", " + pos.z);
                Instantiate(bulletPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
            }
        }

        // Spawn game enders
        for (int i = 0; i < gameEnderCount; i++)
        {
            Position pos = GetUniquePosition(shuffledPositions);
            if (pos != null)
            {
                Debug.Log("Spawning game ender at: " + pos.x + ", " + pos.y + ", " + pos.z);
                Instantiate(gameEnderPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
            }
        }
    }

    public Position GetUniquePosition(List<Position> positions)
    {
        foreach (Position pos in positions)
        {
            int index = collectableData.possiblePositions.IndexOf(pos);
            if (!usedIndices.Contains(index))
            {
                usedIndices.Add(index);
                return pos;
            }
        }
        return null; // No unique position found
    }

    void ShuffleList(List<Position> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Position temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
