using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Waiting : MonoBehaviour
{

    // Show room ID
    // SceneManager.LoadScene("SetupScene");

    public string roomId;
    //public GameManager gameManager;
    private GameObject roomIdText;

    public Slider slider;
    public TMP_Text progressText;

    // If client is ready => load setup scene

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = progress * 100f + "%";

            slider.value = progress;

            yield return null;
        }
    }

}
