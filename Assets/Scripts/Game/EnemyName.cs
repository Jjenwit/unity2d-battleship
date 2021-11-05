using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyName : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<TextMeshProUGUI>().text = gameManager.enemyName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
