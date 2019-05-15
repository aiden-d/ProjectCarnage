using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject connecting;
    public GameObject joinButtons;
    public GameObject leaveButton;
    public GameObject room;
    public TextMeshProUGUI enterCode;
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI playerListText;

    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
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
            playerListText.text = "Player List: \n " +  PhotonNetwork.PlayerListOthers.ToString();
         }
    }
    public override void OnConnectedToMaster() 
    {
        Debug.Log("Connected to master");
        connecting.SetActive(false);
        joinButtons.SetActive(true);

    }

    public void JoinRoom(string name) 
    {
        
        if (name != null) 
        {

            PhotonNetwork.JoinRoom(name);
         }

        Debug.Log(PhotonNetwork.CurrentRoom);
        roomNameText.text = "Code: " + PhotonNetwork.CurrentRoom.Name;
    }
    public void JoinRandomRoom() 
    {
        joinButtons.SetActive(false);
        leaveButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();

        //roomNameText.text = "Code: " + PhotonNetwork.CurrentRoom.Name;
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
        RoomOptions roomOps = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("" + randomRoomRame,roomOps );


    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to crete room");
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        joinButtons.SetActive(false);
        leaveButton.SetActive(true);
        room.SetActive(true);
        Debug.Log("Room Joined");
        roomNameText.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name.ToString();


        base.OnJoinedRoom();
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
