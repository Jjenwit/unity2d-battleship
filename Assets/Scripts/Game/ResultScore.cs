using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<TMP_Text>().text = $"{gameManager.username}  <color=#3166AE>{gameManager.playerScore}</color> - <color=#CA3E3E>{gameManager.enemyScore}</color>  {gameManager.enemyName}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
