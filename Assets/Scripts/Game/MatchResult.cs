using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchResult : MonoBehaviour
{
    GameManager gameManager;
    public GameObject nextButton;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = FindObjectOfType<Camera>();
        if(gameManager.currentRound >= gameManager.rounds || gameManager.enemySurrendered)
        {
            nextButton.SetActive(false);
        } else
        {
            nextButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextClick()
    {
        gameManager.Reset();

        SceneManager.LoadScene("SetupScene");
    }

    public void OnQuitClick()
    {
        gameManager.Reset();

        gameManager.QuitRoom();

        SceneManager.LoadScene("MainMenu");
    }
}
