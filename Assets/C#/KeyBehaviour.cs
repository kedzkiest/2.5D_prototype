using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Gimmick gimmick;

    public int keyNum;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
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
            Destroy(gameObject);
        }
    }
}
