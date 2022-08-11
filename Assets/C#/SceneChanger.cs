using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public enum GameState
    {
        Title,
        Main,
        Result
    }

    public GameState currentGameState;

    public Canvas titleCanvas;
    public Canvas inGameCanvas;
    public Canvas resultCanvas;

    public PencilGenerator[] pencilGenerators;

    public GameObject player;

    private PlayerController_Platform playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        currentGameState = GameState.Title;

        titleCanvas.enabled = true;
        inGameCanvas.enabled = false;
        resultCanvas.enabled = false;

        foreach (PencilGenerator pg in pencilGenerators)
        {
            pg.gameObject.SetActive(false);
        }

        player.SetActive(false);
        playerController = player.GetComponent<PlayerController_Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(currentGameState);
        }

        if (currentGameState == GameState.Title)
        {
            GoToMain();
        }

        if (currentGameState == GameState.Main)
        {
            LevelRetry();
            //BackToTitle();
        }

        if (currentGameState == GameState.Result)
        {
            BackToTitle();
        }
    }

    void GoToMain()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentGameState = GameState.Main;

            titleCanvas.enabled = false;
            inGameCanvas.enabled = true;
            
            foreach (PencilGenerator pg in pencilGenerators)
            {
                pg.gameObject.SetActive(true);
            }

            player.SetActive(true);
        }
    }

    void LevelRetry()
    {
        if(Input.GetKeyDown(KeyCode.R) && playerController.isDead)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void BackToTitle()
    {
        if (Input.GetKey(KeyCode.T))
        {
            currentGameState = GameState.Title;

            titleCanvas.enabled = true;
            inGameCanvas.enabled = false;
            
            foreach (PencilGenerator pg in pencilGenerators)
            {
                pg.gameObject.SetActive(false);
            }

            player.SetActive(false);
        }
    }
}
