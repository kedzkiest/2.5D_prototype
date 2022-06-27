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
    
    // Start is called before the first frame update
    void Start()
    {
        HPBar.value = 1;
        HPText.text = maxHP + " / " + maxHP;
        currentHP = maxHP;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Vector3 damageForce = transform.position - col.GetContact(0).point;
            int damage = Random.Range(1, 100);
            currentHP -= damage;

            HPBar.value = (float) currentHP / (float) maxHP;
            HPText.text = currentHP + " / " + maxHP;
            
            anim.SetTrigger("SmallDamage1");
            rb.AddForce(-50, 0, 0, ForceMode.Acceleration);
        }
    }
}
