using UnityEngine;

public class LeftRight : MonoBehaviour
{
    public float moveSpeed = 2.0f;  // Speed of the left and right movement
    public float minXPosition = -2.0f;  // Minimum X position the object can reach
    public float maxXPosition = 2.0f;  // Maximum X position the object can reach

    private float initialX;  // Initial X position of the object

    void Start()
    {
        initialX = transform.position.x;
    }

    void Update()
    {
        // Move the object left and right using a sine wave pattern
        float xOffset = Mathf.Sin(Time.time * moveSpeed) * ((maxXPosition - minXPosition) / 2);
        Vector3 newPosition = new Vector3(initialX + xOffset, transform.position.y, transform.position.z);

        // Clamp the new position within the defined boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, initialX + minXPosition, initialX + maxXPosition);
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