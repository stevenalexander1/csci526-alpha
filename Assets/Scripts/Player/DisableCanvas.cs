using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace
using Cinemachine;
using System.Collections.Generic;

public class DisableCanvas : MonoBehaviour
{
    [SerializeField] private Image image;  // Change to UI Image
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

    private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    void Start()
    {
        if (image == null)
        {
            Debug.LogError("Image reference is not set. Attach an Image component to the script.");
            return;
        }

        // Find all CinemachineVirtualCamera instances in the scene
        CinemachineVirtualCamera[] allVirtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        virtualCameras.AddRange(allVirtualCameras);
    }

    void Update()
    {
        bool anyCameraActive = false;

        // Check if any of the virtual cameras (except the PlayerFollowCamera) are active
        foreach (CinemachineVirtualCamera virtualCamera in virtualCameras)
        {
            if (virtualCamera != playerFollowCamera && virtualCamera.isActiveAndEnabled)
            {
                anyCameraActive = true;
                break;
            }
        }

        // Disable the Image component if any camera (except PlayerFollowCamera) is active, otherwise enable it
        image.enabled = !anyCameraActive;
    }
}