using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyButton : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isReady)
        {
            GetComponentInParent<Canvas>().enabled = true;
        } else
        {
            GetComponentInParent<Canvas>().enabled = false;
        }
    }

    public void OnClick()
    {
        gameManager.Ready();
        GetComponentInChildren<TMP_Text>().text = "Waiting...";
        GetComponent<Button>().enabled = false;
    }
}
