﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(Rigidbody))]

[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
    {
    public float health;

    public float walkSpeed = 1;
    public float attackDelay;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;

    //states
    [HideInInspector] public bool isAttacking;
    [HideInInspector]public bool isBlocking = false;
    [HideInInspector]public bool isWalking = true;

    //
    private Animator m_anim;
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private Rigidbody m_Rigidbody;
    private bool m_Jump;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
        {
        m_anim = GetComponent<Animator>();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }

    }

    // Update is called once per frame
    void Update()
        {

        checkStates();
        States();
        checkFall();
            
    }
    private void FixedUpdate()
    {

        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");




        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
        #if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
        #endif

        // pass all parameters to the character control script
        Vector3 move  = m_Move;
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void checkFall() 
    {


        Vector3 at = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, 5f))
        {

            Debug.DrawRay(at, transform.up * -1, Color.white, 5.0f);
        }
        else
        {

            m_Rigidbody.velocity = transform.up * -5;
        }
 }
    void checkStates() 
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        if (h == 0 && v == 0 )
        {
            isWalking = false;
        }
        else if (isBlocking || isAttacking)
        {
            isWalking = false;
        }
        else { isWalking = true; }
    }
    public void Attack()
    {
        isWalking = false;
        isAttacking = true;
        m_anim.SetTrigger("Attack_Fast");
        Wait(attackDelay);
        isAttacking = false;

    }
    void States() 
    {
        m_anim.SetBool("Block", isBlocking);
        m_anim.SetFloat("WalkSpeed",walkSpeed * 0.5f);
        m_anim.SetBool("Walk", isWalking);
        if (isWalking) 
        {
            m_Rigidbody.velocity = transform.forward * walkSpeed;
        }
        else 
        {
            m_Rigidbody.velocity = transform.forward * 0 ;

        }



 }
   
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
}

