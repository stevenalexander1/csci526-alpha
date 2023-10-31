using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isGravityInverted = false;
    
    public bool IsGravityInverted
    {
        get => _isGravityInverted;
        set => _isGravityInverted = value;
    }
    
    // Start is called before the first frame update
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            _gameManager.CameraManager.CameraChangedEvent += HandleCameraChangedEvent;
        }
    }

    private void OnEnable()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InvertGravity()
    {
        _isGravityInverted = !_isGravityInverted;
        _gameManager.PlayerCharacter.gameObject.transform.Rotate(new Vector3(0, 0, 180));

    }
    
    private void HandleCameraChangedEvent(GameObject cam)
    {
        if (null == cam) return;
        if (cam == _gameManager.CameraManager.PlayerFollowCamera && _isGravityInverted)
        {
            InvertGravity();
            return;
        }
        SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();
        if (securityCameraComponent != null && securityCameraComponent.DoesInvertGravity)
        {
            InvertGravity();
        }

        
    }
}
