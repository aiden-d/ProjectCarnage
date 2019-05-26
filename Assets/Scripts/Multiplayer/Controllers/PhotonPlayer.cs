using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    public string path = "PhotonPrefabs";
    public string playerPath = "Char_Demo";
    
    public int lifeCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Spawn();
    }


    public void Spawn() 
    {
        int spawnPicker = Random.Range(0, GameSetup.gs.spawnPoints.Length);
        if (PV.IsMine && lifeCount > 0)
        {
            lifeCount--;
            myAvatar = PhotonNetwork.Instantiate(Path.Combine(path, ES3.Load<string>("character")), GameSetup.gs.spawnPoints[spawnPicker].position,
                GameSetup.gs.spawnPoints[spawnPicker].rotation, 0);

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
