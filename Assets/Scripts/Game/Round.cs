using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Round : MonoBehaviour
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
        GetComponent<TMP_Text>().text = $"Round: {gameManager.currentRound}/{gameManager.rounds}";
    }
}
