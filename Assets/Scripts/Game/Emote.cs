using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    GameManager gameManager;
    
    public GameObject heart;
    public GameObject angry;
    public GameObject thumb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.currentEmote == "heart")
        {
            GameObject instance = Instantiate(heart);
            Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            gameManager.currentEmote = "";
        }
        if (gameManager.currentEmote == "angry")
        {
            GameObject instance = Instantiate(angry);
            Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            gameManager.currentEmote = "";
        }
        if (gameManager.currentEmote == "thumb")
        {
            GameObject instance = Instantiate(thumb);
            Destroy(instance, instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            gameManager.currentEmote = "";
        }
    }

    public void OnHeartClick()
    {
        gameManager.SendEmote("heart");
    }

    public void OnAngryClick()
    {
        gameManager.SendEmote("angry");
    }

    public void OnThumbClick()
    {
        gameManager.SendEmote("thumb");
    }
}
