using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerFOV : MonoBehaviour
{
    private float _range;
    private float _angle;
    
    public GameObject _player;
    public LayerMask targetMask; // Contains the player's layer
    public LayerMask obstructionMask; // Contains any obstruction layer

    private GameManager gameManager;
    private GameObject _mainCamera;
    private bool _canSeePlayer;
    private GameObject _light;

    private bool _isGameOver = false; // Track game over state

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        _range = gameObject.GetComponentInChildren<Light>().range;
        _angle = gameObject.GetComponentInChildren<Light>().spotAngle;
        _light = gameObject.transform.GetChild(1).gameObject;
    }

    void Start()
    {
        StartCoroutine(DetectFOV());
        gameManager = _player.GetComponent<GameManager>();
        SendToGoogle.guardExists = true;
    }

    private IEnumerator MakeLightVisible()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        _light.SetActive(true);
        yield return wait;
        _light.SetActive(false);
    }

    private IEnumerator DetectFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        if(_isGameOver)
        {
            return;
        }

        //Collider[] rangeCheck = new Collider[1];
        //if (Physics.OverlapSphereNonAlloc(transform.position, _range, rangeCheck, targetMask) == 1) // Checks if there is an object with targetMask in given radius
        //{
        //    Transform target = rangeCheck[0].transform;

        List<Collider> rangeCheck = new List<Collider>();
        rangeCheck.AddRange(Physics.OverlapSphere(transform.position, _range, targetMask));
        if (rangeCheck.Count > 0) // Checks if there is an object with targetMask in given radius
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) // Check if a raycast is not hitting an obstruction
                {
                    // Analytics 4: Player vs Guard
                    SendToGoogle.setIsGuardDeath(true);

                    _canSeePlayer = true;
                    _player.GetComponent<PlayerCharacter>().ChangeCurrentStealthValue(-10);
                    StartCoroutine(MakeLightVisible());
                }
                else
                    _canSeePlayer = false;
            }
            else
                _canSeePlayer = false;
        }
        else if(_canSeePlayer)
            _canSeePlayer = false;

    }

    private void Update()
    {
        CameraManager cameraManager = _mainCamera.GetComponent<CameraManager>(); 
        if(!_isGameOver)
        {
            if (!cameraManager.PlayerCameraActive) // Makes light visible if using a camera
                _light.SetActive(true);
            else
                _light.SetActive(false);
        }
    }
}
