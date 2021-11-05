using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameManager gameManager;
    private EnemyTiles tile;
    private EnemyTiles previousTile;
    public bool canAttack;
    
    public Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isMyTurn)
        {
            previousTile = tile;
            tile = null;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                tile = targetObject.GetComponent<EnemyTiles>();

                if (tile && !tile.isAttacked)
                {
                    tile.gameObject.GetComponent<SpriteRenderer>().color = targetColor;
                }
            }

            if (Input.GetMouseButtonUp(0) && tile)
            {
                if(!tile.isAttacked && canAttack)
                {
                    gameManager.Attack("" + tile.row + tile.col);
                    canAttack = false;
                }
            }

            if (previousTile != tile && previousTile != null)
            {
                previousTile.gameObject.GetComponent<SpriteRenderer>().color = previousTile.initialColor;
            }
        }
    }

}
