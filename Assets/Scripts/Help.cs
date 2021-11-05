using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Help : MonoBehaviour
{
    public Image helpButton;
    public GameObject helpPanel;
    public GameObject backDrop;
    GameManager gameManager;

    public bool isActive;

    public Sprite helpIcon;
    public Sprite closeIcon;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        helpPanel.SetActive(false);
        backDrop.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClick()
    {
        isActive = !isActive;
        helpPanel.SetActive(isActive);
        backDrop.SetActive(isActive);
        Attack attack = gameManager.GetComponent<Attack>();

        if (isActive)
        {
            helpButton.sprite = closeIcon;
            attack.enabled = false;
        }
        else
        {
            helpButton.sprite = helpIcon;
            attack.enabled = true;
        }
    }

}
