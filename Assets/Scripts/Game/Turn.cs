using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turn : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameEnded)
        { 
            if(gameManager.isMyTurn)
            {
                GetComponent<TMP_Text>().text = "Your turn";
            } else
            {
                GetComponent<TMP_Text>().text = "Opponent's turn";
            }
        }
    }
}
