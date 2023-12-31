using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holographic : MonoBehaviour
{
    // Add this script to the parent of the holographic object
    private GameObject obj;
    private GameManager _gameManager;

    private void Awake()
    {
        obj = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        
        _gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            _gameManager.CameraManager.CameraChangedEvent += HandleCameraChangedEvent;
        }
    }

    private void OnDisable()
    {
        if (_gameManager != null)
        {
            _gameManager.CameraManager.CameraChangedEvent -= HandleCameraChangedEvent;
        }
    }

    private void HandleCameraChangedEvent(GameObject cam)
    {
        if (cam == null) return;
        if (cam == _gameManager.CameraManager.PlayerFollowCamera)
        {
            obj.SetActive(false);
            return;
        }
        SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();
        if (securityCameraComponent != null && securityCameraComponent.IsHolographic)
        {
            obj.SetActive(true);
        }
    }
}
