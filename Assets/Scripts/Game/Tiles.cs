using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public int row;
    public int col;

    public bool isOccupied = false;
    private List<Collider2D> collidingObjs = new List<Collider2D>();

    public bool isAttacked;
    public bool isHit;
    public bool isMissed;

    public GameObject cross;
    public GameObject greyCross;

    private Color initialColor;
    public Color snappedColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(collidingObjs.Count > 0)
        {
            isOccupied = true;
        } else
        {
            isOccupied = false;
        }

        if(isOccupied)
        {
            gameObject.GetComponent<SpriteRenderer>().color = snappedColor;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = initialColor;
        }

        if (isHit)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingObjs.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidingObjs.Remove(collision);
    }
}
