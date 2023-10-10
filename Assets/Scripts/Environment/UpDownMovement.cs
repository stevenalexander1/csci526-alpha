using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;  // Speed of the up and down movement
    public float minHeight = 1.0f;  // Minimum height the object can reach
    public float maxHeight = 4.0f;  // Maximum height the object can reach

    private float initialY;  // Initial Y position of the object

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        // Move the object up and down using a sine wave pattern
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * ((maxHeight - minHeight) / 2);
        Vector3 newPosition = new Vector3(transform.position.x, initialY + yOffset, transform.position.z);

        // Clamp the new position within the defined boundaries
        newPosition.y = Mathf.Clamp(newPosition.y, initialY - minHeight, initialY + maxHeight);
        transform.position = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the player here
            Debug.Log("Collision with Player!");
        }
    }
}