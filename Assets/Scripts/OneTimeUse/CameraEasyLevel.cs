using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script for handling Door Button in CameraEasyLevel

public class CameraEasyLevel : MonoBehaviour
{
    private GameObject _mainCamera;
    private float _distanceThreshold = 6f;
    private Camera _playerCamera;
    private DoorController _doorController;
    Keyboard keyboard;


    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _playerCamera = _mainCamera.GetComponent<Camera>();
        keyboard = Keyboard.current;
    }

    void Update()
    {
        if (keyboard == null) return;
        if (!keyboard.eKey.wasPressedThisFrame) return;
        Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out var hit) && hit.transform.CompareTag("DoorButton") &&
            Vector3.Distance(transform.position, hit.transform.position) < _distanceThreshold)
        {
            if (hit.transform.parent != null)
            {
                GameObject sibling = hit.transform.parent.Find("Door").gameObject;
                _doorController = sibling.GetComponent<DoorController>();
                _doorController.OpenDoor();
            }
        }
    }
}