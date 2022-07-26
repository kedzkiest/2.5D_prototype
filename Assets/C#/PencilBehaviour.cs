using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PencilBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public float lifeTime;

    public int mode;
    
    private float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        if(mode != 3) transform.localScale = new Vector3(0.3f, 0.3f, Random.Range(0.1f, 0.6f));
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifeTime)
        {
            Destroy(gameObject);
        }

        if (mode == 1)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (mode == 2)
        {
            transform.position += transform.forward * moveSpeed * 0.5f * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (mode == 3) return;
        
        if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
