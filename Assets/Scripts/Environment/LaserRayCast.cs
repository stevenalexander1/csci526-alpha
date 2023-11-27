
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRayCast : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float laserLength = 100f;
    [SerializeField] private Vector3 laserDirection = Vector3.right;
    [SerializeField] private bool enableMovement = true;
    [SerializeField] private float laserSpeed = 1.0f; // Add a speed variable
    private PlayerCharacter _playerCharacter;
    private GameObject _mainCamera;
    private bool _isGameOver = false;
    private GameManager _gameManager;

<<<<<<< Updated upstream
=======
    [SerializeField] private bool isElectricCam = false;
    public bool IsElectricCam => isElectricCam;
    private Vector3 _initialPosition;
    float currentTime = 0f;


>>>>>>> Stashed changes
    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _isGameOver = false;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;

        _gameManager = GameManager.Instance;
        _playerCharacter = _gameManager.PlayerCharacter;
        SendToGoogle.laserExists = true;
    }

    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        Vector3 endPoint = startPoint.position + laserDirection * laserLength;
        lr.SetPosition(1, endPoint);

        if (Physics.Raycast(startPoint.position, laserDirection, out var hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.transform.parent.gameObject == _playerCharacter.gameObject
                 || hit.transform.gameObject == _playerCharacter.gameObject)
            {
                SendToGoogle.setIsLaserDeath(true);

                _playerCharacter.ChangeCurrentStealthValue(-Time.deltaTime);
            }
        }

        if (enableMovement)
        {
            float laserSpeedMultiplier = laserSpeed * Time.deltaTime; // Calculate the speed factor

            // Move the cube and laser vertically
            if (laserDirection == Vector3.left || laserDirection == Vector3.right)
            {
                float yOffset = Mathf.Sin(currentTime) * 0.5f * laserSpeedMultiplier;
                transform.Translate(Vector3.up * yOffset);
            }

            // Move the cube and laser horizontally
            if (laserDirection == Vector3.up || laserDirection == Vector3.down)
            {
                float xOffset = Mathf.Sin(currentTime) * 0.5f * laserSpeedMultiplier;
                transform.Translate(Vector3.right * xOffset);
            }

            // Increment your custom time variable
            currentTime += Time.deltaTime;
        }

    }
<<<<<<< Updated upstream
=======

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
        if(isElectricCam)
        {
            if (cam == _gameManager.CameraManager.PlayerFollowCamera)
            {
                enableMovement = false;
                return;
            }
            SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();

            if (securityCameraComponent != null && securityCameraComponent.IsElectric)
            {
                transform.position = _initialPosition;
                enableMovement = true;
            }
            else
            {
                transform.position = _initialPosition;
                enableMovement = false;
            }

        }
    }
>>>>>>> Stashed changes
}

