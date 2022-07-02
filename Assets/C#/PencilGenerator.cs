using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject pencil;

    public int generateMode;
    
    public Vector3 positionOffset;
    public float maxGenerateTime;
    public float minGenerateTime;

    public Material[] pencilColors;

    private Material pencilColor;
    
    private float generateTime;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = (maxGenerateTime + minGenerateTime) / 2.0f;
        //pencilColor = pencil.transform.GetChild(1).GetComponent<Renderer>().sharedMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        int rand = Random.Range(0, 100);
        if (rand < 5)
        {
            generateTime = Random.Range(minGenerateTime, maxGenerateTime);
        }
        
        
        transform.position = player.transform.position + positionOffset;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > generateTime)
        {
            elapsedTime = 0;

            pencil.transform.GetChild(1).GetComponent<Renderer>().material = pencilColors[Random.Range(0, 4)];
            if (generateMode == 1)
            {
                pencil.GetComponent<PencilBehaviour>().mode = 1;
                Instantiate(pencil, transform.position - new Vector3(0, Random.Range(0.0f, 0.8f), 0),
                    Quaternion.Euler(0, 90, 0));
            }

            if (generateMode == 2)
            {
                pencil.GetComponent<PencilBehaviour>().mode = 2;
                Instantiate(pencil, transform.position + new Vector3(Random.Range(-2.8f, 3.0f), 0, 0), Quaternion.Euler(-90, 90, 0));
            }
        }
        
        
        
    }
}
