using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using SocketIOClient;

public class Lobby : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI currentUsername;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CreateGame()
    {
        SceneManager.LoadScene("CreateGame");
    }

    public void JoinGame()
    {
        SceneManager.LoadScene("JoinGame");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    

}
