using UnityEngine;

public class LaserFunction : MonoBehaviour
{
    public enum MovementType
    {
        LeftRight,
        UpDown
    }

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
        if (movementType == MovementType.LeftRight)
            initialPosition = transform.position.x;
        else if (movementType == MovementType.UpDown)
            initialPosition = transform.position.y;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * ((maxPosition - minPosition) / 2);
        Vector3 newPosition = transform.position;

        if (movementType == MovementType.LeftRight)
        {
            newPosition.x = initialPosition + offset;
            newPosition.x = Mathf.Clamp(newPosition.x, initialPosition + minPosition, initialPosition + maxPosition);
        }
        else if (movementType == MovementType.UpDown)
        {
            newPosition.y = initialPosition + offset;
            newPosition.y = Mathf.Clamp(newPosition.y, initialPosition - minPosition, initialPosition + maxPosition);
        }

        transform.position = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Laser Collide with Player!");
            gameManager.GameOver();
            isGameOver = true;
        }
    }
}
