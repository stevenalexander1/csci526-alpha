using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraComponent : MonoBehaviour
{
    [SerializeField] private GameObject securityCamera;
    [SerializeField] private bool doesPan = false;
    public GameObject SecurityCamera => securityCamera;
    public float rotationSpeed = 20.0f; // Adjust this to control the rotation speed
    private float rotationDuration = 5.0f; // Time to rotate left and right in seconds

    private float rotationTimer = 0.0f;
    private bool rotateRight = true; // Start by rotating right

    private void Update()
    {
        if (doesPan)
        {
            PanCamera();
        }
        
    }

    private void PanCamera()
    {
        // Rotate left or right based on the current direction
        if (rotateRight)
        {
            securityCamera.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else
        {
            securityCamera.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // Update the rotation timer
        rotationTimer += Time.deltaTime;

        // Check if it's time to change rotation direction
        if (rotationTimer >= rotationDuration)
        {
            // Toggle the rotation direction
            rotateRight = !rotateRight;

            // Reset the rotation timer
            rotationTimer = 0.0f;
        }
    }
    
}
  

