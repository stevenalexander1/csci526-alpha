using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorController : MonoBehaviour
{
    public Transform door; // Reference to the door Transform.
    public float doorOpenAmount = 2.0f; // How much the door should open on collision.
    public float doorOpenSpeed = 1.0f; // Speed at which the door opens.

    private bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = door.position;
        targetPosition = initialPosition + Vector3.right * doorOpenAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen || !other.CompareTag("Player")) return;

        StartCoroutine(OpenDoorAndDestroyButton());
    }

    private IEnumerator OpenDoorSmoothly()
    {
        float journeyLength = Vector3.Distance(door.position, targetPosition);
        float startTime = Time.time;

        while (Time.time < startTime + journeyLength / doorOpenSpeed)
        {
            float journeyFraction = (Time.time - startTime) * doorOpenSpeed / journeyLength;
            door.position = Vector3.Lerp(initialPosition, targetPosition, journeyFraction);
            yield return null;
        }

        door.position = targetPosition;
    }

    private IEnumerator OpenDoorAndDestroyButton()
    {
        yield return StartCoroutine(OpenDoorSmoothly()); // Wait for the door to open.
        Destroy(gameObject); // Destroy the button GameObject after the door is open.
    }
}