using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Image settingButton;
    public GameObject settingPanel;
    public GameObject themeDropDown;
    public GameObject soundSlider;
    public GameObject mainMenuButton;
    public GameObject backDrop;
    GameManager gameManager;

    public bool isActive;

    public Sprite settingIcon;
    public Sprite closeIcon;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        settingPanel.SetActive(false);
        backDrop.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        if(SceneManager.GetActiveScene().name == "Landing")
        {
            mainMenuButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void OnButtonClick()
    {
        isActive = !isActive;
        settingPanel.SetActive(isActive);
        backDrop.SetActive(isActive);
        Attack attack = gameManager.GetComponent<Attack>();

        if (isActive)
        {
            settingButton.sprite = closeIcon;
            attack.enabled = false;
        }
        else
        {
            settingButton.sprite = settingIcon;
            attack.enabled = true;
        }
    }

    public void OnThemeChange()
    {
        int index = themeDropDown.GetComponent<TMP_Dropdown>().value;
        string theme = "orange";
        switch (index)
        {
            case 0:
                theme = "orange";
                break;
            case 1:
                theme = "blue";
                break;
            case 2:
                theme = "green";
                break;
        }
        gameManager.theme = theme;
    }

    public void OnVolumeChange()
    {
        gameManager.volume = soundSlider.GetComponent<Slider>().value;
    }

    public void OnMainMenuClick()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "SetupScene")
        {
            gameManager.Reset();
            gameManager.QuitRoom();
        }

        gameManager.sceneToLoad = "MainMenu";
    }
}
