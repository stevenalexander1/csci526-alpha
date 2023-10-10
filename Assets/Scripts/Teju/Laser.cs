using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; // Reference to the Rigidbody component
    [SerializeField] private float moveSpeed = 2.0f; // Adjust this to control the speed of movement

    private float maxY = 3.0f; // Adjust this to set the maximum height
    private float minY = 1.0f; // Adjust this to set the minimum height
    private bool movingUp = true;

    private bool isGameOver = false; // Track game over state

    [SerializeField] private GameObject gameOverCanvas; // Reference to the game over canvas

    private void Start()
    {
        isGameOver = false;
        gameOverCanvas.SetActive(false); // Initially hide the game over canvas
    }

    private void Update()
    {
        if (isGameOver)
        {
            return; // Don't update anything if the game is over.
        }

        // Move the laser using Rigidbody's velocity
        Vector3 velocity = rb.velocity;
        if (movingUp)
        {
            velocity.y = moveSpeed;
            if (transform.position.y >= maxY)
            {
                movingUp = false;
            }
        }
        else
        {
            velocity.y = -moveSpeed;
            if (transform.position.y <= minY)
            {
                movingUp = true;
            }
        }
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the player
            ShowGameOverCanvas();
        }
    }

    private void ShowGameOverCanvas()
    {
        isGameOver = true;
        rb.isKinematic = true; // Freeze the laser's movement
        gameOverCanvas.SetActive(true); // Show the game over canvas
        // Optionally, you can add code here to pause the game or perform other game over actions.
    }
}
