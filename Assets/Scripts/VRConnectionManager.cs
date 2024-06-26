using Photon.Pun;
using TMPro;
using UnityEngine;

public class VRConnectionManager : MonoBehaviourPunCallbacks
{
    public TMP_Text connectionStatusText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new Photon.Realtime.RoomOptions(), Photon.Realtime.TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
    }

    void SpawnPlayer()
{
    Vector3 spawnPosition = new Vector3(0, 0.7f, 0);
    PhotonNetwork.Instantiate("VRPlayerPrefab", spawnPosition, Quaternion.identity);
}
}