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
    public void Hit(float damage, float knockback, Transform direction)
    {
        healthMultiplier += damage;
        Debug.Log("Hit");
        anim.SetTrigger("Hit");
        rb.velocity = direction.forward * knockback * healthMultiplier;
    }
 
}
