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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            anim.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            playerController.isDead = true;
            HPText.text = "Game Over";
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Pencil") && !playerController.isDamaged)
        {
            //Vector3 damageForce = transform.position - col.GetContact(0).point;
            int damage = 10;
            currentHP -= damage;

            HPBar.value = (float) currentHP / (float) maxHP;
            HPText.text = currentHP + " / " + maxHP;
            
            anim.SetTrigger("SmallDamage1");
            rb.AddForce(-50, 0, 0, ForceMode.VelocityChange);
            
            playerController.isDamaged = true;
            Invoke(nameof(PlayerRecovery), recoveryTime);
            
            Destroy(col.gameObject, 0.1f);
        }

        if (col.gameObject.CompareTag("Pencil") && playerController.isDamaged)
        {
            Destroy(col.gameObject);
        }
    }

    void PlayerRecovery()
    {
        playerController.isDamaged = false;
    }
}
