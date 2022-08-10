using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tips : MonoBehaviour
{
    public GameObject player;

    public Gimmick gimmick;

    public GameObject[] tipsPos;

    private TextMeshProUGUI tipsText;
    
    // Start is called before the first frame update
    void Start()
    {
        tipsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTipsText();
    }

    void UpdateTipsText()
    {
        if (Vector3.Distance(tipsPos[0].transform.position, player.transform.position) <= 1.0f &&
            !gimmick.hasKey[0])
        {
            tipsText.text = "You need a pink key to remove this wall.";
        }
        else if (Vector3.Distance(tipsPos[1].transform.position, player.transform.position) <= 1.0f &&
                 !gimmick.hasKey[1])
        {
            tipsText.text = "You need a blue key to remove this wall.";
        }
        else
        {
            tipsText.text = "";
        }
    }
}
