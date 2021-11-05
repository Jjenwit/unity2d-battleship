using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    public Color orange;
    public Color blue;
    public Color green;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.theme)
        {
            case "orange":
                spriteRenderer.color = orange;
                break;
            case "blue":
                spriteRenderer.color = blue;
                break;
            case "green":
                spriteRenderer.color = green;
                break;
        }
    }
}
