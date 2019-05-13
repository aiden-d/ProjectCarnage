using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class InputManager : MonoBehaviour
{
    public string attackAxis;
    public string attackSlowAxis;
    public string blockAxis;
    public string rollAxis;

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
            
            Attack();
        }
        if (Input.GetAxis(attackSlowAxis) > 0)
        {

            AttackSlow();
        }

        if (Input.GetAxis(blockAxis) > 0)
        {
            Block();
        }
        else
        if (Input.GetAxis(rollAxis)>0)
        {

            Roll();
        }
        {
            StopBlock();
        }
    }
    public void Block()
    {
        if (controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fast") || controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slow") || controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") )
        {

        }
        else
        {

            controller.m_anim.SetBool("Block", true);
        }
    }
    public void Roll()
    {
        if (controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fast") || controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slow") || controller.m_anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {

        }
        else
        {
            controller.m_anim.SetTrigger("Roll");
        }
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
    public void AttackSlow()
    {
        if (controller.isAttacking == false)
        {
            controller.AttackSlow();
        }

    }

    

}
