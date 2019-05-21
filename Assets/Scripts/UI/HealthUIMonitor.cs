using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HealthUIMonitor : MonoBehaviourPunCallbacks
{
    public string tag = "Player";


    public GameObject[] playersInOrder;
    public GameObject[] foundPlayers1;
    public HealthUIObject[] UIObjects;

    public string[] playerIDS;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);

        if (players.Length != playerIDS.Length) 
        {
            assignID();
        }


        playersInOrder = getPlayersInOrder(players);


        int i = 0; 
        while (i < UIObjects.Length) 
        {
            if (i<playersInOrder.Length && playersInOrder[i] != null) 
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

        GameObject[] inOrder = new GameObject[foundPlayers.Length];
        int i = 0;
        while (i < playerIDS.Length) 
        {
            int ii = 0;
           
            while(ii<foundPlayers.Length) 
            {
                char[] c = foundPlayers[ii].GetComponent<PhotonView>().ViewID.ToString().ToCharArray();
                char[] d = playerIDS[i].ToCharArray();
                if (c[0] == d[0]) 
                {
                    inOrder[i] = foundPlayers[ii];
                }
                ii++;
            }
            i++;
        }
        return inOrder;
    }


    void assignID() 
    {
        StartCoroutine(wait());



    }
    IEnumerator wait() 
    {
        yield return new WaitForSeconds(2);

        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);

        playerIDS = new string[players.Length];


        int i = 0;
        while (i < players.Length)
        {
            playerIDS[i] = players[i].GetComponent<PhotonView>().ViewID.ToString();

            i++;
        }


    }
    public override void OnEnable()
    {
        assignID();
    }








}
