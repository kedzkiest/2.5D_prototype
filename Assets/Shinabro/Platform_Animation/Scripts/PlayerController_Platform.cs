using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Platform : MonoBehaviour
{
    Animator anim;

    [Header("Rotation speed")] public float speed_rot;
    
    [Header("Jump power")] public float jumpPower;

    [Header("Movement speed during jump")] public float speed_move;

    [Header("Time available for combo")] public int term;
    
    [Header("cool time for attack")] public float coolTime;
    
    [Header("cool time for jump")] public float jumpCoolTime;
    
    [Header("cool time for dodge")] public float dodgeCoolTime;

    public bool isJump;
    public bool isDodge;
    public bool isDamaged;
    public bool isDead;

    private CapsuleCollider playerCollider;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void FixedUpdate()
    {
        // judge grounded
        if (Physics.Raycast(transform.position, Vector3.down, 0.51f, 31))
        {
            isJump = false;
            anim.SetBool("Landing", true);
        }
        else
        {
            isJump = true;
            anim.SetBool("Landing", false);
        }

        //change the size of collider
        if (anim.GetBool("Block") || isDodge)
        {
            playerCollider.center = new Vector3(0, 2.5f, 0);
            playerCollider.radius = 2.5f;
        }
        else
        {
            playerCollider.center = new Vector3(0, 4, 0);
            playerCollider.radius = 4;
        }

        if (isDamaged || isDead)
        {
            anim.SetBool("Run", false);
            return;
        }
        
        Rotate();
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

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

    Quaternion rot;
    bool isRun;


    void Rotate()
    {

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            Move();
            rot = Quaternion.LookRotation(Vector3.right);
        }


        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            Move();
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

        if (isJump)
        {
            transform.position += transform.forward * speed_move * Time.deltaTime;
            anim.SetBool("Run", false);
        }
        else
        {
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

    private float dodgeTimer;
    void Dodge()
    {
        dodgeTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dodgeTimer >= dodgeCoolTime)
        {
            dodgeTimer = 0;
            
            anim.SetTrigger("Dodge");
            //playerCollider.enabled = false;
            //Invoke(nameof(EnableCollider), 0.03f);
            
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

    private float jumpTimer;
    void Jump()
    {
        jumpTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimer >= jumpCoolTime)
        {
            jumpTimer = 0;
            
            GetComponent<Rigidbody>().AddForce(0, jumpPower, 0, ForceMode.Impulse);
            //playerCollider.enabled = false;
            //Invoke(nameof(EnableCollider), 0.03f);
            anim.SetTrigger("Jump");
            anim.SetBool("Landing", false);
            isJump = true;
        }
    }
    
    void JumpEnd()
    {
        isJump = false;
    }

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
}
