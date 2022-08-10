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

    public DangerManager dangerManager;

    private Material pencilColor;
    
    private float generateTime;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = (maxGenerateTime + minGenerateTime) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // when a pencil rain comes, pencils won't fly sideways
        if (generateMode == 1 && dangerManager.isDanger)
        {
            return;
        }

        RandomlyChoosePencilGenerateTime();

        MaintainRelativePosToPlayer();

        GeneratePencil();
    }

    void RandomlyChoosePencilGenerateTime()
    {
        int rand = Random.Range(0, 100);
        if (rand < 5)
        {
            if (!dangerManager.isDanger)
            {
                generateTime = Random.Range(minGenerateTime, maxGenerateTime);
            }
            else
            {
                generateTime = Random.Range(minGenerateTime * 0.01f, maxGenerateTime * 0.01f);
            }
        }
    }

    void MaintainRelativePosToPlayer()
    {
        transform.position = player.transform.position + positionOffset;
    }

    void GeneratePencil()
    {
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

    public void SetGenerateTime(float t)
    {
        generateTime = t;
    }
}
