using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayersReady : MonoBehaviour
{
    public int playersReady = 0;

    PhotonView pv;
    PhotonLobbyPlayer player;
    public TextMeshProUGUI text;

    GameObject[] lobbyPlayers;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
       if (PhotonNetwork.IsMasterClient) 
       { 
            if (PhotonNetwork.InRoom) 
            { 
                if (pv == null) 
                {
                    lobbyPlayers = GameObject.FindGameObjectsWithTag("LobbyPlayer");
                    int i = 0;
                    while (i < lobbyPlayers.Length)
                    {
                        if (lobbyPlayers[i].GetComponent<PhotonView>().IsMine) 
                        {
                            pv = lobbyPlayers[i].GetComponent<PhotonView>();
                        }
                        player = pv.gameObject.GetComponent<PhotonLobbyPlayer>();
                        i++;

                    }



                }
                lobbyPlayers = GameObject.FindGameObjectsWithTag("LobbyPlayer");
                int ii = 0;
                int readyCount = 0;
                while (ii < lobbyPlayers.Length)
                {
                    if (lobbyPlayers[ii].GetComponent<PhotonLobbyPlayer>().mySelectedCharacter != null) 
                    {
                        readyCount++;
                    }
                    ii++;

                }
                text.text = readyCount  +"/"+  lobbyPlayers.Length + " Players Ready";
            }


       } 
    }

    


}

