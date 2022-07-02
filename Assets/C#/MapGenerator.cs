using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int size;

    private List<GameObject> boxes = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boxes.Add(box);

            if (i > 0)
            {
                box.transform.position = new Vector3(i,
                    boxes.ElementAt(i - 1).transform.position.y + Random.Range(-0.15f, 0.3f), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
