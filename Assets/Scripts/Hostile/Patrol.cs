using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float startWaitTime;
    private float waitTime;
    public Transform locations;

    private List<Transform> moveSpots = new List<Transform>();
    private int index = 1;
    private int increment = 1;

    // Variables for smooth rotation
    [SerializeField] private float rotationSpeed = 2.0f; // Adjust the rotation speed as needed
    private Quaternion targetRotation;

    void Start()
    {
        foreach (Transform child in locations)
        {
            moveSpots.Add(child); // Adding patrol locations to the list
        }
        waitTime = startWaitTime;
        if (moveSpots.Count == 0)
            return;
        transform.LookAt(moveSpots[index]);
    }

    void Update()
    {
        if (moveSpots.Count == 0)
            return;

        // Calculate the direction to the next patrol point
        Vector3 targetDirection = moveSpots[index].position - transform.position;

        // Smoothly interpolate the rotation to the new look direction
        targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        transform.position = Vector3.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[index].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (index == 0 || index == moveSpots.Count - 1)
                    increment *= -1;
                index += increment;
                waitTime = startWaitTime;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }
}