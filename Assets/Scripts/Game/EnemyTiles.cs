using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTiles : MonoBehaviour
{
    GameManager gameManager;

    public int row;
    public int col;

    public bool isAttacked;
    public bool isHit;
    public bool isMissed;

    public Color initialColor;

    public GameObject cross;
    public GameObject greyCross;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit)
        {
            GameObject crossObj = Instantiate(cross);
            crossObj.transform.position = transform.position;
            isHit = false;
        }

        if (isMissed)
        {
            GameObject crossObj = Instantiate(greyCross);
            crossObj.transform.position = transform.position;
            isMissed = false;
        }

        if(!gameManager.isMyTurn)
        {
            GetComponent<SpriteRenderer>().color = initialColor;
        }
    }

}
