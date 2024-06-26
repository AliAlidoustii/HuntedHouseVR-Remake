using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ObjectController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public GameObject[] cubes = new GameObject[15];

    void Start()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(false); // Ensure the cubes are disabled by default
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode >= 1 && eventCode <= 15) // Enable cubes
        {
            cubes[eventCode - 1].SetActive(true);
        }
        else if (eventCode >= 100 && eventCode <= 114) // Disable cubes
        {
            cubes[eventCode - 100].SetActive(false);
        }
    }

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
