using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class InputManager : MonoBehaviour
{
    public string attackAxis;
    public string blockAxis;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(attackAxis) > 0)
        {
            Debug.Log("Atack");
            Attack();
        }
        if (Input.GetAxis(blockAxis) > 0)
        {
            Block();
        }
        else
        {
            StopBlock();
        }
    }
    public void Block()
    {
        controller.isWalking = false;
        controller.isBlocking = true;
    }
    public void StopBlock()
    {
        controller.isBlocking = false;
    }

    public void Attack()
    {
        if (controller.isAttacking == false)
        {
            controller.Attack();
        }
        
    }
    
}
