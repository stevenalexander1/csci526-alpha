using System.Collections;
using UnityEngine;

public class SecurityCameraComponent : MonoBehaviour
{
    [SerializeField] private GameObject securityCamera;

    [Header("Camera Pan")]
    [SerializeField] private bool rotateCamera = true; // Add this line to control camera rotation
    private bool startNextRotation = true;
    bool rotRight = false;
    [SerializeField] private float yaw = 40;
    [SerializeField] private float secondsToRot = 4;

    [Header("Camera UI")] [SerializeField] private GameObject cameraUIButton;
    
    public GameObject SecurityCamera => securityCamera;

    void Update()
    {
        if (startNextRotation && rotateCamera)
        {
            if (rotRight)
                StartCoroutine(Rotate(yaw, secondsToRot));
            else
                StartCoroutine(Rotate(-yaw, secondsToRot));
        }
        FaceCamera();

    }

    IEnumerator Rotate(float yaw, float duration)
    {
        startNextRotation = false;
        Quaternion initialRotation = transform.rotation;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.down);
            yield return null;
        }
        startNextRotation = true;
        rotRight = !rotRight;
    }
    
    private void FaceCamera()
    {
        // Have the item value text face the opposite direction of the camera
        cameraUIButton.transform.LookAt(Camera.main.transform);
        cameraUIButton.transform.Rotate(0, 180, 0);
    }
}