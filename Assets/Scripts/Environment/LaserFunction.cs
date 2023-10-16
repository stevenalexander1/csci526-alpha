using System;
using UnityEngine;
using System.Collections;
public class LaserFunction : MonoBehaviour
{
    public enum MovementType
    {
        LeftRight,
        UpDown
    }

    [SerializeField] private GameObject lasersGameObject;  // Serialized field to hold the all lasers GameObject
    private int originalLayer;

    public MovementType movementType;
    public float moveSpeed = 2.0f;
    public float minPosition;
    public float maxPosition;

    private float initialPosition;
    private bool isGameOver = false;

    [SerializeField]
    private GameManager gameManager;

    void Start()
    {
        isGameOver = false;
        originalLayer = lasersGameObject.layer;
        if (movementType == MovementType.LeftRight)
            initialPosition = transform.position.x;
        else if (movementType == MovementType.UpDown)
            initialPosition = transform.position.y;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * ((maxPosition - minPosition) / 2);
        Vector3 newPosition = transform.position;

        switch (movementType)
        {
            case MovementType.LeftRight:
                newPosition.x = initialPosition + offset;
                newPosition.x = Mathf.Clamp(newPosition.x, initialPosition + minPosition, initialPosition + maxPosition);
                break;
            case MovementType.UpDown:
                newPosition.y = initialPosition + offset;
                newPosition.y = Mathf.Clamp(newPosition.y, initialPosition - minPosition, initialPosition + maxPosition);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position = newPosition;
    }
    
    private IEnumerator ChangeLayerCoroutine()
    {
        // Change the layer to Default (layer 0)
        lasersGameObject.layer = 0;

        // Wait for a moment (e.g., 2 seconds)
        yield return new WaitForSeconds(2f);

        // Change the layer back to the original layer
        lasersGameObject.layer = originalLayer;
    }


}
