using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CreateGame : MonoBehaviour
{
    public string roomStatus;
    public Toggle publicToggle;
    public Toggle privateToggle;
    public GameManager gameManager;

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Create()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.CreateGame();
        SceneManager.LoadScene("Waiting");
    }

    public void PublicToggle()
    {
        if (publicToggle.isOn )
        {
            roomStatus = "public";
            privateToggle.isOn = false;
            //Debug.Log(roomStatus);
        }
    }

    public void PrivateToggle()
    {
        if (privateToggle.isOn)
        {
            roomStatus = "private";
            publicToggle.isOn = false;
            //Debug.Log(roomStatus);
        }
    }
}
