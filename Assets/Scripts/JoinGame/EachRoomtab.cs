using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachRoomtab : MonoBehaviour
{
    GameManager gameManager;
    public string roomId;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void onClicked()
    {
        gameManager.JoinRoomWithId(roomId);
    }
}
