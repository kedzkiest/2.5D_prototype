using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject wallCenter;

    public Gimmick gimmick;

    public int wallNum;

    public float downHeight;

    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
        DOTween.SetTweensCapacity(500, 50);
    }

    // Update is called once per frame
    void Update()
    {
        UnlockWall();
        RemoveWall();
    }

    void UnlockWall()
    {
        if (Vector3.Distance(wallCenter.transform.position, player.transform.position) <= 1.0f &&
            gimmick.hasKey[wallNum])
        {
            isLocked = false;
        }
    }

    void RemoveWall()
    {
        if (isLocked) return;

        transform.DOMoveY(downHeight, 1.0f).SetEase(Ease.Linear);
        if (transform.position.y <= downHeight) DOTween.Kill(transform);
    }
}
