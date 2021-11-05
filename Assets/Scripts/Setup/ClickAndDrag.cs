using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickAndDrag : MonoBehaviour
{
    GameManager gameManager;
    public GameObject selectedObject;
    public float snapRange;
    Vector3 offset;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && targetObject.GetComponent<Ship>() != null)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
                selectedObject.GetComponent<Ship>().isSnapped = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && selectedObject)
        {
            Ship ship = selectedObject.GetComponent<Ship>();
            ship.gameObject.transform.Rotate(new Vector3(0, 0, 90));
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {

            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
            Transform head = selectedObject.transform.GetChild(0);
            Transform tail = selectedObject.transform.GetChild(1);

            float minDistanceToHead = -1;
            GameObject closestObjectToHead = null;

            float minDistanceToTail = -1;
            GameObject closestObjectToTail = null;

            foreach (GameObject obj in tiles)
            {
                float distanceToHead = Vector3.Distance(head.position, obj.transform.position);

                if (closestObjectToHead == null || distanceToHead < minDistanceToHead)
                {
                    closestObjectToHead = obj;
                    minDistanceToHead = distanceToHead;
                }

                float distanceToTail = Vector3.Distance(tail.position, obj.transform.position);

                if (closestObjectToTail == null || distanceToTail < minDistanceToTail)
                {
                    closestObjectToTail = obj;
                    minDistanceToTail = distanceToTail;
                }
            }

            Ship selectedShip = selectedObject.GetComponent<Ship>();

            if (closestObjectToHead != null && closestObjectToTail != null && minDistanceToHead < snapRange && minDistanceToTail < snapRange && selectedShip.currentTiles.Count >= 4)
            {

                if (gameManager.shipsTiles.Intersect(selectedShip.currentTiles).Count() == 0)
                {
                    selectedObject.transform.position = closestObjectToHead.transform.position + selectedObject.transform.position - head.position;
                    selectedObject.GetComponent<Ship>().isSnapped = true;
                }
            }

            selectedObject = null;
        }
    }

}
