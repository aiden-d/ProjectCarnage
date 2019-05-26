using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class AttackScript : MonoBehaviour
{
    
    public GameObject root;
    public float damage;
    public float knockback;
    public float heavyDamage;
    public float heavyKnockback;
    GameCharController controller;
    private void OnEnable()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        controller = root.GetComponent<GameCharController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Shield" || controller.isSlowAttack) 
        {
            if (controller.isSlowAttack)
            {
                if (collision.gameObject.transform.root != root.transform)
                {
                    GameObject root;
                    if (collision.collider.transform.tag == "Shield") root = collision.collider.GetComponent<ShieldScript>().root;
                    else root = collision.collider.gameObject;
                    PhotonView id = root.GetComponent<PhotonView>();
                    Debug.Log(root.name + " hit");
                    id.RPC("Hit", RpcTarget.All, heavyDamage, heavyKnockback, root.transform.forward);
                    Debug.Log("Collision");
                }
            }
            if (controller.isAttacking)
            {

                if (collision.gameObject.transform.root != root.transform)
                {
                    PhotonView id = collision.collider.gameObject.GetComponent<PhotonView>();
                    Debug.Log(collision.collider.gameObject.name + " hit");
                    id.RPC("Hit", RpcTarget.All, damage, knockback, root.transform.forward);
                    Debug.Log("Collision");
                }
            }
            
        }

    }
}
