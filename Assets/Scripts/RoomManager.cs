using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    public string roomName = "Room";

    public GameObject player;
    [Space]
    public Transform spawnPoint;
    [Space]
    public GameObject conectcam;


    void Awake()
    {
        Instance = this;


    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting..");
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("We're in the lobby");
        
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
        Debug.Log("We're connected and in a room!");

       conectcam.SetActive(false);

        Spawnplayer();
    }


    public void Spawnplayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<health>().isLocalPlayer = true;
    }
}
