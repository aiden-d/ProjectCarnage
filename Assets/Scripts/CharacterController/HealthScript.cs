using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class HealthScript : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    //public bool isBlocking;

    public float healthMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void Hit(float damage, float knockback, Vector3 forward)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            healthMultiplier += damage;
            Debug.Log("Hit");
            anim.SetTrigger("Hit");
            rb.velocity = forward * knockback * healthMultiplier;
        }
       
    }
 
}
