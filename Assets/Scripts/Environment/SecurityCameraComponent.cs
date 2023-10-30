using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SecurityCameraComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;

    [Header("Camera")]
    [SerializeField] private GameObject securityCamera;
    [SerializeField] private bool doesInvertGravity = false;
    [SerializeField] private bool isHolographic = false;

    [Header("Camera Pan")]
    [SerializeField] private bool rotateCamera = true;
    [SerializeField] private float rotationDuration = 5.0f; // Default duration for rotation
    [SerializeField] private float waitDuration = 2.0f; // Duration to wait at each position
    [SerializeField] private float maxRotationAngle = 50.0f; // Maximum rotation angle in degrees
    private bool isRotating = false;
    private bool isWaiting = false; // Add isWaiting to control waiting
    private float rotationStartTime;
    private float waitStartTime;
    private int waitState = 0; // 0: Not waiting, 1: Waiting at an extreme angle, 2: Waiting in the middle
    private Quaternion initialRotation;

    [Header("Camera UI")]
    [SerializeField] private GameObject cameraUIButton;

    public GameObject SecurityCamera => securityCamera;
    public bool DoesInvertGravity => doesInvertGravity;
    public bool IsHolographic => isHolographic;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        rotationStartTime = Time.time;
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (rotateCamera && !isRotating)
        {
            if (isWaiting)
            {
                if (Time.time - waitStartTime >= waitDuration)
                {
                    isWaiting = false;
                    rotationStartTime = Time.time;
                    if (waitState == 1)
                    {
                        // Transition from waiting at an extreme angle to the middle
                        waitState = 2;
                        RotateCamera(0);
                    }
                    else if (waitState == 2)
                    {
                        // Transition from waiting in the middle to an extreme angle
                        waitState = 1;
                        RotateCamera(isWaiting ? maxRotationAngle : -maxRotationAngle);
                    }
                }
            }
            else
            {
                if (waitState == 0)
                {
                    // Start with an initial rotation
                    RotateCamera(maxRotationAngle);
                    waitState = 1;
                }
                else
                {
                    // Rotate from one extreme angle to the other
                    waitState = 0;
                    RotateCamera(isWaiting ? maxRotationAngle : -maxRotationAngle);
                }
            }
        }
        FaceCamera();
    }

    void RotateCamera(float targetAngle)
    {
        isRotating = true;
        rotationStartTime = Time.time;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0);

        StartCoroutine(PerformRotation(startRotation, endRotation, rotationDuration));
    }

    IEnumerator PerformRotation(Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        isRotating = false;
        isWaiting = true;
        waitStartTime = Time.time;
    }

    private void FaceCamera()
    {
        cameraUIButton.transform.LookAt(Camera.main.transform);
        cameraUIButton.transform.Rotate(0, 180, 0);
    }
}
