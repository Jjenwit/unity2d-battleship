using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwaper : MonoBehaviour
{

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.sceneToLoad != null && gameManager.sceneToLoad != "")
        {
            SceneManager.LoadScene(gameManager.sceneToLoad);
        }

        gameManager.sceneToLoad = null;
    }
}
