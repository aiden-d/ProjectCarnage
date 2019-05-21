using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    //UI
    public GameObject connecting;
    public GameObject joinButtons;
    public GameObject leaveButton;
    public GameObject room;
    public GameObject startGameButton;
    public TextMeshProUGUI enterCode;
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI playerListText;
    public TextMeshProUGUI usernameText;

    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        startGameButton.SetActive(false);
        joinButtons.SetActive(false);
        connecting.SetActive(true);
        leaveButton.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
        room.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom) 
        {
            PhotonNetwork.NickName = usernameText.text;
            room.SetActive(true);
            
            string players = "";
            foreach (KeyValuePair<int, Photon.Realtime.Player> kvp  in PhotonNetwork.CurrentRoom.Players)
            {
                players += "\n" + kvp.Value.NickName;

            }
            playerListText.text = "Players in lobby: " + players;

        }
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
            
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }
    public override void OnConnectedToMaster() 
    {
        Debug.Log("Connected to master");
        PhotonNetwork.AutomaticallySyncScene = true;
        connecting.SetActive(false);
        joinButtons.SetActive(true);

    }

    public void JoinRoom(string name) 
    {
        
        if (name != null) 
        {
            Debug.Log("Name: " + name);
            RoomOptions roomOps = new RoomOptions();
            roomOps.IsVisible = false;
            PhotonNetwork.JoinOrCreateRoom(name, roomOps, TypedLobby.Default);
         }

        Debug.Log(PhotonNetwork.CurrentRoom);
        
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room");
        CreateRoom();
       
    }
    public void CreateRoom() 
    {

        int randomRoomRame = Random.Range(0,100000);
        Debug.Log("RoomName = " + randomRoomRame);
        RoomOptions roomOps = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};
        roomOps.PublishUserId = true;
        PhotonNetwork.CreateRoom("" + randomRoomRame,roomOps );


    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to crete room");
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        joinButtons.SetActive(false);
        leaveButton.SetActive(true);
        room.SetActive(true);
        Debug.Log("Room Joined");
        roomNameText.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name.ToString();


       
    }
    public void leaveRoom() 
    {
        room.SetActive(false);
        joinButtons.SetActive(true);

        leaveButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
        Debug.Log("left room");
    }
    public void OnButtonJoinGame() 
    {
        JoinRoom(enterCode.text);
     }



}
