using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FinishGameScript : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject VictoryUI;
    public GameObject DefeatUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        if (g.Length == 1) 
        {
            GameUI.SetActive(false);
            if (g[0].GetComponent<PhotonView>().IsMine) 
            {
                VictoryUI.SetActive(true);
            }
            else 
            {
                DefeatUI.SetActive(true);
            }

        }
    }
}
