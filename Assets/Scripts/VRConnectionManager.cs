using Photon.Pun;
using TMPro;
using UnityEngine;

public class VRConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text connectionStatus;
    [SerializeField] private string roomName = "Room1";
    [SerializeField] private GameObject playerPrefab;

    private enum ConnectionState { Disconnected, Connecting, Connected, InLobby, InRoom }
    private ConnectionState currentState = ConnectionState.Disconnected;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        currentState = ConnectionState.Connected;
       // connectionStatus.text = "Connected to master";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        currentState = ConnectionState.InLobby;
       // connectionStatus.text = "Joined lobby";
        PhotonNetwork.JoinOrCreateRoom(roomName, new Photon.Realtime.RoomOptions(), Photon.Realtime.TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        currentState = ConnectionState.InRoom;
       // connectionStatus.text = "Joined room";
        SpawnPlayer();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        currentState = ConnectionState.Disconnected;
       // connectionStatus.text = "Disconnected";
    }

    private void SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(0, 0.7f, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }
}