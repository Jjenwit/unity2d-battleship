using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<TextMeshProUGUI>().text = gameManager.username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
