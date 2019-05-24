using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonLobbyPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;

    public CharacterSelect mySelectedCharacter;
    public string selectedCharacterString;
    public int playersReady;

    SelectionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        pv = GetComponent<PhotonView>();
        playersReady = 0;
        manager = GameObject.Find("Selection Screen").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        mySelectedCharacter = manager.activeSelect;
        if (pv.IsMine) 
        {
            selectedCharacterString = mySelectedCharacter.name;
        }

    }

   





    [PunRPC]
    public void UpdateCharacterSelect() 
    {
        manager.UpdateCharacters();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(selectedCharacterString);
        }
        else
        {
            // Network player, receive data
            this.selectedCharacterString = (string)stream.ReceiveNext();
        }
    }
}
