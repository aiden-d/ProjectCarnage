using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Animator))]
public class GameCharController : MonoBehaviour
{
    public bool enableControls = true;
    public float health;

    public float walkSpeed = 1;
    public float attackDelay;
    public float attackSlowDelay;

    public float rollSpeed =2;
    public float rollAnimSpeed = 2;
    public float attackFastAnimSpeed = 1;
    public float attackSlowAnimSpeed = 1;

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;

    //states
    [HideInInspector] public bool isSlowAttack;
   [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isBlocking = false;
    [HideInInspector] bool isWalking = true;

    public Collider shieldCollider;
    public GameObject shield;
    public bool shieldCoversBody = false;
    //
     public Animator m_anim;
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private Rigidbody m_Rigidbody;
    private bool m_Jump;
    private PhotonView pv;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal = new Vector3(0, 0, 0);

    [HideInInspector] public bool falling;

    CapsuleCollider playerCollider;
    InputManager input;
    // Start is called before the first frame update
    void OnEnable()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            this.enabled = false;
        }
        input = GetComponent<InputManager>();
        m_anim = GetComponent<Animator>();
        m_anim.SetFloat("RollSpeed", rollAnimSpeed);
        m_anim.SetFloat("Attack_Slow_Speed", attackSlowAnimSpeed);
        m_anim.SetFloat("Attack_Fast_Speed", attackFastAnimSpeed);
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        playerCollider = gameObject.GetComponent<CapsuleCollider>();

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
        
            if (enableControls)
            {

                // read inputs
                float h = input.moveInput().x;
                float v = input.moveInput().y;




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
                Vector3 move = m_Move;
                if (move.magnitude > 1f) move.Normalize();
                move = transform.InverseTransformDirection(move);

                move = Vector3.ProjectOnPlane(move, m_GroundNormal);
                m_TurnAmount = Mathf.Atan2(move.x, move.z);
                m_ForwardAmount = move.z;
                float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
                transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
            }
        
    }

    void checkFall() 
    {


        Vector3 at = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, 5f))
        {

            Debug.DrawRay(at, transform.up * -1, Color.white, 5.0f);
            falling = falling;
        }
        else
        {
            falling = true;
            m_Rigidbody.velocity = transform.up * -5;
        }
 }
    void checkStates() 
    {
        if (enableControls)
        {

            float h = input.moveInput().x;
            float v = input.moveInput().y;

            if (h == 0 && v == 0)
            {
                isWalking = false;
            }
            else if (isBlocking || (isAttacking && isSlowAttack == false))
            {
                isWalking = false;
                m_Rigidbody.velocity = transform.forward * 0;
            }
            else { isWalking = true; }
        }
        else isWalking = false;
 
    }
    
    void States() 
    {
        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            isAttacking = false;
            isWalking = false;

        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Fast"))
        {
            isAttacking = true;
            isWalking = false;
            m_Rigidbody.velocity = transform.forward * 0;
        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slow"))
        {
            isAttacking = true;
            isWalking = false;
            isSlowAttack = true;
        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            m_Rigidbody.velocity = transform.forward * rollSpeed;

            isAttacking = false;
            isSlowAttack = false;

        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
        {
            if (shield != null) shield.SetActive(true);
            if (shieldCoversBody)
            {
                playerCollider.enabled = false;
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                
            } 
            shieldCollider.enabled = true;
            m_Rigidbody.velocity = transform.forward * 0;
        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            m_Rigidbody.velocity = transform.forward * 0;

            isAttacking = false;
            isSlowAttack = false;
        }
        
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            m_Rigidbody.velocity = transform.forward * walkSpeed;

            isAttacking = false;
            isSlowAttack = false;
        }

        if (!m_anim.GetCurrentAnimatorStateInfo(0).IsName("Block")) 
        {
            if (shield != null) shield.SetActive(false);
            if (shieldCoversBody)
            {
                playerCollider.enabled = true;
                m_Rigidbody.constraints = RigidbodyConstraints.None;
                m_Rigidbody.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            }
            shieldCollider.enabled = false;
        }

        float f = 0;
        
        m_anim.SetFloat("WalkSpeed",walkSpeed * 0.5f);
        m_anim.SetBool("Walk", isWalking);
        



 }
    public void Attack()
    {
        
        m_anim.SetTrigger("Attack_Fast");
       
        

    }
    public void AttackSlow()
    {
        
        
        m_anim.SetTrigger("Attack_Slow");
        
        

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
    
}


