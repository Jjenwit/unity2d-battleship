using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    //public GameObject LoadingScreen;
    public Slider slider;
    public TMP_Text progressText;
    public TMP_Text roomIdText;
    public GameManager gameManager;
    //public Button readyButton;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        string roomId = gameManager.roomId;
        roomIdText.text = "Room ID : " + roomId;
        //Debug.Log(roomId);
    }

    
    public void Update()
    {
        LoadLevel(5);
    }
    

    public void LoadLevel(int sceneIndex)
    {
        gameManager = FindObjectOfType<GameManager>();
        bool valid = gameManager.isValidWaiting;
        if (valid)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = progress * 100f + "%";

            slider.value = progress;

            yield return null;
        }
    }

    public void CopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = gameManager.roomId;
    }
}
