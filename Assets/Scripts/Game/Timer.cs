using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    GameManager gameManager;
    public float timeRemaining;
    public bool isMyTurn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        RestartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining >= 0 && !gameManager.gameEnded && !gameManager.isPaused)
        {
            timeRemaining -= Time.deltaTime;
            GetComponent<TMP_Text>().text = "Time: " + Mathf.Ceil(timeRemaining);
        }
    }

    public void RestartTimer()
    {
        timeRemaining = gameManager.time;
    }
}
