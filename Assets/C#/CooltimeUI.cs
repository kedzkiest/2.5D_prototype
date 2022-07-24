using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooltimeUI : MonoBehaviour
{
    public PlayerController_Platform playerController;

    public enum Action
    {
        Jump,
        Dodge
    }

    public Action action;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (image.fillAmount < 1)
        {
            image.color = Color.yellow;
            SetOpacity(image, 0.5f);
        }
        else
        {
            image.color = Color.white;
            SetOpacity(image, 1);
        }

        if (action == Action.Jump)
        {
            image.fillAmount = playerController.jumpTimer / playerController.jumpCoolTime;
        }

        if (action == Action.Dodge)
        {
            image.fillAmount = playerController.dodgeTimer / playerController.dodgeCoolTime;
        }
    }

    void SetOpacity(Image image, float alpha)
    {
        Color c = image.color;
        image.color = new Color(c.r, c.g, c.b, alpha);
    }
}
