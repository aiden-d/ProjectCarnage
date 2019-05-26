using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class SpawnPlayer : MonoBehaviour
{
    public string path = "PhotonPrefabs";
    public string playerPath = "PhotonNetworkedPlayer";
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Instantiating");
        PhotonNetwork.Instantiate(Path.Combine(path, playerPath), new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
