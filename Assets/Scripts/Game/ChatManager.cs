using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public GameObject content;
    public GameObject textObj;
    public TMP_InputField input;
    private GameManager gameManager;

    public Color playerColor;
    public Color enemyColor;

    private string username = "";
    private string message = "";

    // Start is called before the first frame update
    void Start()
    {
        gameManager= FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(username != "" && message != "")
        {
            GameObject newText = Instantiate(textObj, content.transform);
            string hexColor = (gameManager.username == username) ? ColorUtility.ToHtmlStringRGB(playerColor) : ColorUtility.ToHtmlStringRGB(enemyColor);
            newText.GetComponent<TMP_Text>().text = $"<color=#{hexColor}>{username}: </color>" + message;
            username = "";
            message = "";
        } 


       if(input.text != "")
       {
            if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                gameManager.SendMessageToServer(input.text);
                input.text = "";
            }
       } 
    }

    public void ShowMessage(string username, string message)
    {
        this.username = username;
        this.message = message;
    }
}
