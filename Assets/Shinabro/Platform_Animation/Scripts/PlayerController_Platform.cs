using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Platform : MonoBehaviour
{
    Animator anim;

    [Header("Rotation speed")] public float speed_rot;
    
    [Header("Jump power")] public float jumpPower;
    
    [Header("Dodge power")] public float dodgePower;

    [Header("Movement speed during jump")] public float jumpSpeed_move;
    
    [Header("Movement speed during running")] public float speed_move;

    [Header("Time available for combo")] public int term;
    
    [Header("cool time for attack")] public float coolTime;
    
    [Header("cool time for jump")] public float jumpCoolTime;
    
    [Header("cool time for dodge")] public float dodgeCoolTime;

    public bool isJump;
    public bool isDodge;
    public bool isDamaged;
    public bool isDead;

    public JumpPadManager jumpPadManager;

    private CapsuleCollider playerCollider;
    private float rayLength = 0.5f;
    private Rigidbody rb;
    private float elapsedTime;
    
    public float currentLine;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);

        elapsedTime = 0;
        currentLine = 0;
    }

    private void LateUpdate()
    {
        elapsedTime += Time.deltaTime;
        
        dodgeTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;
        
        if (!jumpPadManager.isJumping)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, currentLine);
        }
        else
        {
            currentLine = transform.position.z;
            return;
        }
        
        // judge grounded
        Debug.DrawRay(transform.position, Vector3.down, Color.blue, rayLength);
        Debug.DrawRay(transform.position, new Vector3(1, -1, 0), Color.red, rayLength);
        Debug.DrawRay(transform.position, new Vector3(-1, -1, 0), Color.green, rayLength);
        
        if (Physics.Raycast(transform.position, Vector3.down, rayLength, 31) ||
            Physics.Raycast(transform.position, new Vector3(1, -1, 0), rayLength, 31) ||
            Physics.Raycast(transform.position, new Vector3(-1, -1, 0), rayLength, 31))
        {
            isJump = false;
            anim.SetBool("Landing", true);
        }
        else
        {
            //isJump = true;
            anim.SetBool("Landing", false);
        }

        //change the size of collider when dodge / crouch
        if (anim.GetBool("Block") || isDodge)
        {
            playerCollider.center = new Vector3(0, 2.45f, 0);
            playerCollider.radius = 2;
            playerCollider.height = 5;
        }
        else
        {
            playerCollider.center = new Vector3(0, 4, 0);
            playerCollider.radius = 4;
            playerCollider.height = 8;
        }

        if (isDamaged || isDead)
        {
            anim.SetBool("Run", false);
            return;
        }

        if (elapsedTime <= 1.5f) return;
        Rotate();
        Move();

        // prevent rolling-jump (unintentional behaviour) 
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (!isJump)
        {
            // available when not jumping, can cancel dodge
            //Jump();

            if (!isDodge)
            {
                // available when not jumping, cannot cancel dodge
                Jump();
                
                Block();

                Dodge();
                
                if (!anim.GetBool("Block"))
                {
                    // available when not crouching
                    Attack();
                } 
            }
            /*
            Skill1();

            Skill2();

            Skill3();

            Skill4();

            Skill5();

            Skill6();

            Skill7();

            Skill8();
            */
        }

        if (anim.GetBool("Block") && isJump)
        {
            anim.SetBool("Block", false);
        }
    }

    [HideInInspector]
    public Quaternion rot;
    bool isRun;


    void Rotate()
    {

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            rot = Quaternion.LookRotation(Vector3.right);
        }


        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            rot = Quaternion.LookRotation(Vector3.left);
        }

        else
        {
            anim.SetBool("Run", false);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed_rot * Time.deltaTime);

    }


    void Move()
    {
        if (anim.GetBool("Block")) return;

        if (isJump && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            transform.position += transform.forward * jumpSpeed_move * Time.deltaTime;
            anim.SetBool("Run", false);
        }
        
        if (!isJump && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            transform.position += transform.forward * speed_move * Time.deltaTime;
            anim.SetBool("Run", true);
        }
    }

    int clickCount;
    float timer;
    bool isTimer;


    void Attack()
    {

        if (isTimer)
        {
            timer += Time.deltaTime;
        }


        if (Input.GetMouseButtonDown(0))
        {
            switch (clickCount)
            {

                case 0:

                    anim.SetTrigger("Attack1");

                    isTimer = true;

                    clickCount++;
                    break;


                case 1:

                    if (timer <= coolTime) break;
  
                    if (timer <= term)
                    {
                        anim.SetTrigger("Attack2");

                        clickCount++;
                    }


                    else
                    {
                        anim.SetTrigger("Attack1");

                        clickCount = 1;
                    }


                    timer = 0;
                    break;


                case 2:

                    if (timer <= coolTime) break;
                    
                    if (timer <= term)
                    {
                        anim.SetTrigger("Attack3");

                        Invoke(nameof(ResetClickCount), coolTime);

                        isTimer = false;
                    }


                    else
                    {
                        anim.SetTrigger("Attack1");

                        clickCount = 1;
                    }

                    timer = 0;
                    break;
            }
        }
    }

    void ResetClickCount()
    {
        clickCount = 0;
    }

    [HideInInspector]
    public float dodgeTimer;
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dodgeTimer >= dodgeCoolTime)
        {
            if (transform.localEulerAngles.y <= 100)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(dodgePower, 0, 0, ForceMode.Impulse);
            }
            else
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(-dodgePower, 0, 0, ForceMode.Impulse);
            }
            
            dodgeTimer = 0;
            
            anim.SetTrigger("Dodge");
            anim.SetBool("Block", false);

            isDodge = true;
            Invoke(nameof(DodgeEnd), 0.5f);
        }
    }
    
    void DodgeEnd()
    {
        isDodge = false;
    }

    void Block()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Block", true);
        }
        else
        {
            anim.SetBool("Block", false);
        }
    }

    [HideInInspector]
    public float jumpTimer;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimer >= jumpCoolTime && anim.GetBool("Landing"))
        {
            jumpTimer = 0;

            shortRay();
            Invoke(nameof(longRay), 1);

            transform.Translate(0, 0.5f, 0);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            anim.SetBool("Landing", false);
            isJump = true;
        }
    }
    
    void shortRay()
    {
        rayLength = 0.1f;
    }

    void longRay()
    {
        rayLength = 0.5f;
    }

    /*
    void EnableCollider()
    {
        GetComponent<CapsuleCollider>().enabled = true;
    }

    // Skill1
    void Skill1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Play Skill1 animation
            anim.SetTrigger("Skill1");
        }
    }

    // Skill2
    void Skill2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Play Skill2 animation
            anim.SetTrigger("Skill2");
        }
    }

    // Skill3
    void Skill3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Play Skill3 animation
            anim.SetTrigger("Skill3");
        }
    }

    // Skill4
    void Skill4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Play Skill4 animation
            anim.SetTrigger("Skill4");
        }
    }

    // Skill5
    void Skill5()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // Play Skill5 animation
            anim.SetTrigger("Skill5");
        }
    }

    // Skill6
    void Skill6()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // Play Skill6 animation
            anim.SetTrigger("Skill6");
        }
    }

    // Skill7
    void Skill7()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // Play Skill7 animation
            anim.SetTrigger("Skill7");
        }
    }

    // Skill8
    void Skill8()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            // Play Skill8 animation
            anim.SetTrigger("Skill8");
        }
    }
    */
}
