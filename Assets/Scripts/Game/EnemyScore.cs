using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScore : MonoBehaviour
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
        GetComponent<TMP_Text>().text = $"<color=#CA3E3E>{gameManager.enemyGameScore.ToString()}</color>";
    }
}
