using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoveKey : MonoBehaviour
{
    public GameObject[] waypoints;
    public float[] reactDistance;

    public float[] jumpPower;
    public int[] jumpNum;
    public float[] jumpDuration;
    public Ease[] jumpEase;

    public GameObject player;

    private int currentPosition;
    private bool isCalledOnce;

    private float dist;

    private float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
        isCalledOnce = false;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (currentPosition == waypoints.Length - 1) return;
        
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= reactDistance[currentPosition] && !isCalledOnce)
        {
            isCalledOnce = true;
            currentPosition++;
            transform.DOJump(waypoints[currentPosition].transform.position, jumpPower[currentPosition],
                jumpNum[currentPosition], jumpDuration[currentPosition]).SetEase(jumpEase[currentPosition]);
            Invoke(nameof(DisableIsCalledOnce), jumpDuration[currentPosition]);
        }
    }

    void DisableIsCalledOnce()
    {
        isCalledOnce = false;
    }
}
