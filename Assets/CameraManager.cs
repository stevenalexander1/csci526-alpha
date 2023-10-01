using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    
    [SerializeField] private GameObject playerFollowCamera;
    private GameObject _currentActiveCamera;
    
    public GameObject PlayerFollowCamera => playerFollowCamera;
    public GameObject CurrentActiveCamera => _currentActiveCamera;
    public bool PlayerCameraActive => _currentActiveCamera == playerFollowCamera;
    private void Start()
    {
        // Find all Cinemachine Virtual Cameras in the scene
        CinemachineVirtualCamera[] allVirtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        virtualCameras.AddRange(allVirtualCameras);

        // Activate PlayerFollowCamera
        ActivateCameraByName("PlayerFollowCamera");
    }

    public void ActivateCameraByName(string cameraName)
    {
        // Deactivate all cameras
        DeactivateAllCameras();

        // Find and activate the camera with the specified name
        CinemachineVirtualCamera targetCamera = virtualCameras.Find(cam => cam.gameObject.name == cameraName);
        if (targetCamera != null)
        {
            GameObject o;
            (o = targetCamera.gameObject).SetActive(true);
            _currentActiveCamera = o;
        }
        else
        {
            Debug.LogWarning("Camera not found: " + cameraName);
        }
    }
    
    public void ActivateCameraByObject(GameObject cameraObject)
    {
        // Deactivate all cameras
        DeactivateAllCameras();
        Debug.Log("Activating camera: " + cameraObject.name);
        // Activate the specified camera
        cameraObject.SetActive(true);
        _currentActiveCamera = cameraObject;
    }

    public void DeactivateAllCameras()
    {
        Debug.Log("Deactivating all cameras");
        // Deactivate all virtual cameras
        foreach (var camera in virtualCameras)
        {
            camera.gameObject.SetActive(false);
        }
    }
}