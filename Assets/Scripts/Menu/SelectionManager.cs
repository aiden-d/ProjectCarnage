using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES3Types;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class SelectionManager : MonoBehaviour
{
    bool sent = false;
    public CharacterSelect activeSelect;
    public string defualtPath;
    public CharacterSelect[] characters;

    CharacterSelect lastCharSelect = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacters();
       







    }

    public void UpdateCharacters() 
    {
        GameObject[] lobbyPlayers;
        lobbyPlayers = GameObject.FindGameObjectsWithTag("LobbyPlayer");
        foreach (CharacterSelect cs in characters)
        {
            bool taken = false;
            foreach (GameObject g in lobbyPlayers)
            {
                if (g.GetComponent<PhotonLobbyPlayer>().selectedCharacterString == cs.name)
                {
                    if (activeSelect == null || g.GetComponent<PhotonLobbyPlayer>().selectedCharacterString != activeSelect.name) 
                    {
                        taken = true;
                        cs.taken = true;
                    }

                }
            }
            if (taken == false)
            {
                cs.taken = false;
            }
        }
    }

    public void SaveSelection()
    {   
        if (activeSelect != null)
        {
            ES3.Save<string>("character", activeSelect.characterPath);
        }
        else 
        {
            ES3.Save<string>("character", defualtPath);
        }

    }


}
