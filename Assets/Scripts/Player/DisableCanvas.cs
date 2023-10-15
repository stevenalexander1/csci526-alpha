using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class DisableCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;

    private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    void Start()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas reference is not set. Attach a Canvas component to the script.");
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

        // Disable the Canvas if any camera (except PlayerFollowCamera) is active, otherwise enable it
        canvas.enabled = !anyCameraActive;
    }
}