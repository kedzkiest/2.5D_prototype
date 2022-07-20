using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP;

    private int currentHP;

    public Slider HPBar;
    public TextMeshProUGUI HPText;

    private Animator anim;
    private Rigidbody rb;

    private PlayerController_Platform playerController;
    private bool isCalledOnce;

    public float recoveryTime;
    
    // Start is called before the first frame update
    void Start()
    {
        HPBar.value = 1;
        HPText.text = maxHP + " / " + maxHP;
        currentHP = maxHP;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController_Platform>();

        isCalledOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0 && !isCalledOnce)
        {
            isCalledOnce = true;
            anim.SetBool("Dead", true);
            anim.SetTrigger("SmallDamage1");
            //anim.enabled = false;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            playerController.isDead = true;
            HPText.text = "Game Over";
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Pencil") && !(playerController.isDamaged || playerController.isDead))
        {
            //Vector3 damageForce = transform.position - col.GetContact(0).point;
            int damage = 10;
            currentHP -= damage;

            HPBar.value = (float) currentHP / (float) maxHP;
            HPText.text = currentHP + " / " + maxHP;
            
            anim.SetTrigger("SmallDamage1");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(-50, 0, 0, ForceMode.Impulse);
            
            playerController.isDamaged = true;
            Invoke(nameof(PlayerRecovery), recoveryTime);
            
            Destroy(col.gameObject, 0.1f);
        }

        if (col.gameObject.CompareTag("Pencil") && (playerController.isDamaged || playerController.isDead))
        {
            Destroy(col.gameObject);
        }
    }

    void PlayerRecovery()
    {
        playerController.isDamaged = false;
    }
}
