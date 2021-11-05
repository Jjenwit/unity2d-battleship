using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<TMP_Text>().text = (gameManager.winner == gameManager.username) ? "Victory" : "Defeated";
    }

}
