using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public GameObject player;
    
    public GameObject[] sensor;

    public PencilGenerator pencilGenerator;

    public float censorDistance;

    public Sprite dangerSign;
    
    // Start is called before the first frame update
    void Start()
    {
        dangerSign.GameObject().SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in sensor)
        {
            if (Vector3.Distance(go.transform.position, player.transform.position) <= censorDistance)
            {
                
            }
        }
    }
}
