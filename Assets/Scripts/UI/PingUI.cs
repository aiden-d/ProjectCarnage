using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PingUI : MonoBehaviour
{
    public TextMeshProUGUI pingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pingText.text = "Ping = " + PhotonNetwork.GetPing();
    }
}
