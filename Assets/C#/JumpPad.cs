using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class JumpPad : MonoBehaviour
{
    public GameObject goal;

    public float jumpPower;
    
    public float jumpDuration;

    public JumpPadManager jumpPadManager;

    private float elapsedTime;
    
    private bool _isJumping;

    private GameObject player;

    private string[] jumpMotion = {"JumpPad1", "JumpPad2", "JumpPad3"};
    
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (_isJumping && elapsedTime >= jumpDuration)
        {
            _isJumping = false;
            jumpPadManager.isJumping = false;
            player.GetComponent<Animator>().SetBool("usingJumpPad", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;
            FlyPlayerOnCollision();
        }
    }

    void FlyPlayerOnCollision()
    {
        elapsedTime = 0;
        _isJumping = true;
        jumpPadManager.isJumping = true;
        player.transform.DOJump(goal.transform.position, jumpPower, 1, jumpDuration).SetEase(Ease.Linear);

        int r = Random.Range(0, 3);
        player.GetComponent<Animator>().SetTrigger(jumpMotion[r]);
        player.GetComponent<Animator>().SetBool("usingJumpPad", true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<PlayerController_Platform>().rot = Quaternion.LookRotation(Vector3.right);
        
        if (r == 0)
        {
            r = Random.Range(0, 3);
            switch (r)
            {
                case 0:
                    player.transform.DOLocalRotate(player.transform.up * 360, 1f).SetRelative(true);
                    break;
                case 1:
                    player.transform.DOLocalRotate(player.transform.up * -360, 1f).SetRelative(true);
                    break;
                case 2:
                    break;
            }

            return;
        }

        r = Random.Range(0, 7);
        switch (r)
        {
            case 0:
                player.transform.DOLocalRotate(player.transform.right * 360, 1f).SetRelative(true);
                break;
            case 1:
                player.transform.DOLocalRotate(player.transform.right * -360, 1f).SetRelative(true);
                break;
            case 2:
                player.transform.DOLocalRotate(player.transform.up * 360, 1f).SetRelative(true);
                break;
            case 3:
                player.transform.DOLocalRotate(player.transform.up * -360, 1f).SetRelative(true);
                break;
            case 4:
                player.transform.DOLocalRotate(player.transform.forward * 360, 1f).SetRelative(true);
                break;
            case 5:
                player.transform.DOLocalRotate(player.transform.forward * -360, 1f).SetRelative(true);
                break;
            case 6:
                break;
        }
    }
}
