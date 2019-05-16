﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    
    public Joystick joystick;
    public bool isMobile;
    public string attackAxis;
    public string attackSlowAxis;
    public string blockAxis;
    public string rollAxis;

    private PhotonView pv;

    GameCharController controller;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            this.enabled = false;
        }
            joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
            controller = GetComponent<GameCharController>();
        

    }

    // Update is called once per frame
    void Update()
    {

            if (isMobile == false)
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
                {
                    StopBlock();
                }
                if (Input.GetAxis(rollAxis) > 0)
                {

                    Roll();
                }

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
        controller.m_anim.SetBool("Block", false);
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

    //horisontal vertical
    public Vector2 moveInput() 
    {
        Vector2 outVec;
        if (isMobile)
        {


            outVec.x = joystick.Horizontal;
            outVec.y = joystick.Vertical;
        }
        else 
        {
           outVec.x = CrossPlatformInputManager.GetAxis("Horizontal");
           outVec.y = CrossPlatformInputManager.GetAxis("Vertical");
        }
        return outVec;

 }

    

}
