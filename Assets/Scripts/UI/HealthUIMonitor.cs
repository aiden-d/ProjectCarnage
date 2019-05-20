using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HealthUIMonitor : MonoBehaviour
{
    public string tag = "Player";


    public GameObject[] playersInOrder;

    public HealthUIObject[] UIObjects;
    public int maxPlayers = 4;
    public string[] playerIDS = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        //pv.Owner.ActorNumber;   
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        assignID(players);
        playersInOrder = getPlayersInOrder(players);


        int i = 0;
        while (i < UIObjects.Length) 
        {
            if (playersInOrder[i] != null) 
            {

                UIObjects[i].gameObject.SetActive(true);
                UIObjects[i].changeHealth(playersInOrder[i].GetComponent<HealthScript>().healthMultiplier);
            }
            else 
            {
                UIObjects[i].gameObject.SetActive(false);
            }
            i++;
        }

    }

    GameObject[] getPlayersInOrder(GameObject[] foundPlayers) 
    {

        GameObject[] inOrder = new GameObject[maxPlayers];
        int i = 0;
        while (i < playerIDS.Length) 
        {
            int ii = 0;
           
            while(ii<foundPlayers.Length) 
            { 
                if (foundPlayers[ii].GetComponent<PhotonView>().Owner.UserId == playerIDS[i]) 
                {
                    inOrder[i] = foundPlayers[ii];
                }
                ii++;
            }
            i++;
        }
        return inOrder;
    }
    void assignID(GameObject[] foundPlayers) 
    {

        int i = 0;
        while (i < foundPlayers.Length) 
        {
            PhotonView pv = foundPlayers[i].GetComponent<PhotonView>();
            string id = pv.Owner.UserId;

            bool isAssigned = false;

            int ii = 0;
            while (ii < playerIDS.Length) 
            { 
                if (id == playerIDS[ii]) 
                {
                    isAssigned = true;
                }
                else if (playerIDS[i] == null) 
                {

                    playerIDS[ii] = id;
                    isAssigned = true;
                }

                ii++;
            }
            if (isAssigned == false) 
            {

                Debug.LogError("Too many players for UI objects");

            }
            i++;
        }
    }


    
    
}
