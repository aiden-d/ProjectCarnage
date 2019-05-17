using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public float distToDie;
    public string networkPlayerTag;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(0, 0, 0);
        if (Vector3.Distance(transform.position, vec) > distToDie)
        {
            Death();
        }
    }

    public void Death() 
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag(networkPlayerTag);
        GameObject localPlayer = null;
        foreach (GameObject g in go) 
        {
            if (g.GetComponent<PhotonView>().IsMine)
            {
                localPlayer = g;

            }
        }

        localPlayer.GetComponent<PhotonPlayer>().Spawn();
        PhotonNetwork.Destroy(gameObject);
        
    }
}
