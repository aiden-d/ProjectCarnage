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

    Animator anim;
    // Start is called before the first frame update
    void OnEnable()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            this.enabled = false;
        }
            
            controller = GetComponent<GameCharController>();
        anim = GetComponent<Animator>();

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
            isMobile = true;
        }
        else isMobile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("controller null");
            controller = GetComponent<GameCharController>();
        }
        if (controller.falling == true) { StopBlock(); }
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fast") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slow") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") )
        {

        }
        else
        {

            if (controller.falling == false) anim.SetBool("Block", true);
        }
    }
    public void Roll()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fast") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slow") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {

        }
        else
        {
            anim.SetTrigger("Roll");
        }
    }
    public void StopBlock()
    {
        anim.SetBool("Block", false);
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
