using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isGravityInverted = false;
    [SerializeField] public Camera targetCamera; // Assign minimap camera in the Inspector
    [SerializeField] public LayerMask layerToAdd; // roof layer
    [SerializeField] public LayerMask layerToRemove; // ground layer

    private Camera minimap => targetCamera;
    private LayerMask roof => layerToAdd;
    private LayerMask ground => layerToRemove;

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
        if (_isGravityInverted)
        {
            // make roof visible in minimap
            if(minimap != null)
            {
                if(roof != null)
                {
                    minimap.cullingMask |= roof;
                }
                if(ground != null)
                {
                    minimap.cullingMask &= ~ground;
                }
            }
        }
        else
        {
            // make roof disappear in minimap
            if(minimap != null)
            {
                if(roof != null)
                {
                    minimap.cullingMask &= ~roof;
                }
                if(ground != null)
                {
                    minimap.cullingMask |= ground;
                }
            }
        }
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
