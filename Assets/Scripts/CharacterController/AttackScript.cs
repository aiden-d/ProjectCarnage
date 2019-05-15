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
    GameCharController controller;
    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        controller = root.GetComponent<GameCharController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (controller.isAttacking || controller.isSlowAttack)
        {

            if (collision.gameObject.transform.root != root.transform)
            {
                HealthScript hs = collision.gameObject.transform.root.GetComponent<HealthScript>();
                hs.Hit(damage, knockback, root.transform);
                Debug.Log("Collision");
            }
        }
    }
}
