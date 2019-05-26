using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShieldMultiplayerScript : MonoBehaviour, IPunObservable
{
    public bool isShieldActive;
    GameCharController controller;
    CapsuleCollider playerCollider;
    Rigidbody rb;
    public GameObject shield;
    // Start is called before the first frame update
    void OnEnable()
    {
        controller = GetComponent<GameCharController>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Block")) isShieldActive = true;
            else isShieldActive = false;
            stream.SendNext(isShieldActive);
        }
        else
        {
             isShieldActive = (bool)stream.ReceiveNext();

            if (isShieldActive)
            {
                shield.SetActive(true);
                playerCollider.enabled = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }
            else
            {
                shield.SetActive(false);
                playerCollider.enabled = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }

        }

    }
}
