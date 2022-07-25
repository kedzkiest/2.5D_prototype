using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public Gimmick gimmick;

    public int keyNum;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0.1f * Mathf.Sin(Time.time) * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gimmick.hasKey[keyNum] = true;
            if(gameObject.GetComponent<MoveKey>()) gameObject.GetComponent<MoveKey>().DOKill();
            Destroy(gameObject);
        }
    }
}
