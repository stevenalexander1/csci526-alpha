using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser_horizontal : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private float moveSpeed = 2.0f; // Adjust this to control the speed of movement
    [SerializeField]
    private GameManager gameManager;
    private float maxY = 3.0f; // Adjust this to set the maximum height
    private float minY = 1.0f; // Adjust this to set the minimum height
    private bool movingUp = true;
    
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 newPosition = startPoint.position;
        if (movingUp)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
            if (newPosition.y >= maxY)
            {
                newPosition.y = maxY;
                movingUp = false;
            }
        }
        else
        {
            newPosition.y -= moveSpeed * Time.deltaTime;
            if (newPosition.y <= minY)
            {
                newPosition.y = minY;
                movingUp = true;
            }
        }
        startPoint.position = newPosition;

        // Update the laser position
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.back, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.transform.CompareTag("Player"))
            {
                // gameManager.GameOver(); // Call the GameOver function when the player is hit.
                // isGameOver = true; // Set the game over state to true.
            }
        }
        else
        {
            lr.SetPosition(1, -Vector3.back * 5000);
        }
    }

    
}
