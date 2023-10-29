using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    //  Delegates
    public delegate void CameraChangedEventDelegate(GameObject newCamera);
    public CameraChangedEventDelegate CameraChangedEvent;
    
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    
    [SerializeField] private GameObject playerFollowCamera;
    private GameObject _currentActiveCamera;
    private GameObject _lastUsedSecurityCamera;

    public GameObject PlayerFollowCamera => playerFollowCamera;
    public GameObject CurrentActiveCamera => _currentActiveCamera;
    public GameObject LastUsedSecurityCamera => _lastUsedSecurityCamera;

    public SecurityCameraComponent CurrentActiveCameraComponent { get; set; }
    

    public bool PlayerCameraActive => _currentActiveCamera == playerFollowCamera;
    private void Start()
    {
        // Find all Cinemachine Virtual Cameras in the scene
        CinemachineVirtualCamera[] allVirtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        virtualCameras.AddRange(allVirtualCameras);
        playerFollowCamera = GameObject.Find("PlayerFollowCamera");
        // Activate PlayerFollowCamera
        ActivateCameraByObject(playerFollowCamera);
    }

    private void OnEnable()
    {
        CameraChangedEvent += HandleCameraChangedEvent;
    }
    
    private void OnDisable()
    {
        CameraChangedEvent -= HandleCameraChangedEvent;
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
            CameraChangedEvent?.Invoke(o);
        }
        else
        {
            Debug.LogWarning("Camera not found: " + cameraName);
        }
    }
    
    public void ActivateCameraByObject(GameObject cameraObject)
    {
        // Deactivate all cameras
        if (null == cameraObject) return;
        DeactivateAllCameras();
        Debug.Log("Activating camera: " + cameraObject.name);
        // Activate the specified camera
        cameraObject.SetActive(true);
        _currentActiveCamera = cameraObject;
        CameraChangedEvent?.Invoke(cameraObject);
    }
    
    public void ActivateCameraBySecurityCameraComponent(SecurityCameraComponent securityCameraComponent)
    {
        // Deactivate all cameras
        DeactivateAllCameras();
        Debug.Log("Activating camera: " + securityCameraComponent.gameObject.name);
        // Activate the specified camera
        securityCameraComponent.gameObject.SetActive(true);
        _currentActiveCamera = securityCameraComponent.SecurityCamera;
        CameraChangedEvent?.Invoke(securityCameraComponent.SecurityCamera);
    }

    private void DeactivateAllCameras()
    {
        // Deactivate all virtual cameras
        foreach (var cam in virtualCameras)
        {
            cam.gameObject.SetActive(false);
        }
    }
    
    private void HandleCameraChangedEvent(GameObject newCamera)
    {
        if (null == newCamera) return;
        if (newCamera != playerFollowCamera)
        {
            _lastUsedSecurityCamera = newCamera;
        }
    }
    
    
}