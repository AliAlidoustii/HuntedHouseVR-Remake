using UnityEngine;
using Photon.Pun;

public class GameEnderCollectable : MonoBehaviourPun
{
    private static int vrPlayerScore = 0;
    private const int maxScore = 3;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vrPlayerScore++;
            if (uiManager != null)
            {
                uiManager.AddGameEnder();
            }
            CheckWinner("VR Player");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ghost"))
        {
            // Assuming "Ghost" reaching the VR Player ends the game immediately.
            CheckWinner("Ghost");
        }
    }

private void CheckWinner(string playerTag)
{
    if (playerTag == "VR Player" && vrPlayerScore >= maxScore)
    {
        PlayerPrefs.SetString("Winner", "VR Player");
        DestroyVRPlayer();
    }
    else if (playerTag == "Ghost")
    {
        PlayerPrefs.SetString("Winner", "Ghost");
        DestroyVRPlayer();
    }
}

private void DestroyVRPlayer()
{
    GameObject vrPlayer = GameObject.FindGameObjectWithTag("Player");
    if (vrPlayer != null)
    {
        Destroy(vrPlayer);
    }
    PhotonNetwork.LoadLevel("GameOverScene");
}
}
