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
    [SerializeField] private bool enableMovement = true; // Add a checkbox for movement control
    private PlayerCharacter _playerCharacter;
    private GameObject _mainCamera;
    private bool _isGameOver = false;
    private GameManager _gameManager;

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
                // Analytics 3: Player vs Laser
                SendToGoogle.setIsLaserDeath(true);

                _playerCharacter.ChangeCurrentStealthValue(-Time.deltaTime);
            }
        }

        if (enableMovement)
        {
            // Move the cube and laser vertically
            if (laserDirection == Vector3.left || laserDirection == Vector3.right)
            {
                float yOffset = Mathf.Sin(Time.time) * 0.5f; // Adjust the amplitude and speed as needed
                transform.Translate(Vector3.up * yOffset * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.zero); // Stop horizontal movement if laser direction is not (x=-1, y=0, z=0) or (x=1, y=0, z=0)
            }

            // Move the cube and laser horizontally
            if (laserDirection == Vector3.up || laserDirection == Vector3.down)
            {
                float xOffset = Mathf.Sin(Time.time) * 0.5f; // Adjust the amplitude and speed as needed
                transform.Translate(Vector3.right * xOffset * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.zero); // Stop vertical movement if laser direction is not (x=0, y=1, z=0) or (x=0, y=-1, z=0)
            }
        }
    }
}
