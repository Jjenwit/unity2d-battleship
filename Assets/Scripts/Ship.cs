using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public bool isSnapped = false;
    public List<string> currentTiles = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, 5)];
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tiles tile = collision.gameObject.GetComponent<Tiles>();

        if (tile != null)
        {
            currentTiles.Add("" + tile.row +  tile.col);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Tiles tile = collision.gameObject.GetComponent<Tiles>();

        if (tile != null)
        {
            currentTiles.Remove("" +  tile.row + tile.col);
        }
    }

}
