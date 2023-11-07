using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to the electric object
public class ElectricObjects : MonoBehaviour
{
   
   // private GameObject obj;
    private GameManager _gameManager;

    [SerializeField] private GameObject backwardObject;
    [SerializeField] private GameObject forwardObject;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isRotating = false;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float rotationSpeed = 50f;

    private Vector3 _initialPosition;
    private Coroutine _slideCoroutine;
    private Coroutine _slideCoroutine1;
    private GameObject _playerObj;
    private Transform _playerParent;
    private Quaternion _initialRotation;
    private Vector3 _centerPoint;


    public bool IsMoving => isMoving;
    public bool IsRotating => isRotating;
    public GameObject BackwardObject => backwardObject;
    public GameObject ForwardObject => forwardObject;
    public float SlideDuration => slideDuration;
    public float RotationSpeed => rotationSpeed;


    private void Awake()
    {
       

       
    }
    void Start()
    {
        _gameManager = GameManager.Instance;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _playerObj = GameObject.FindWithTag("Player");
        _playerParent = _playerObj.transform.parent;
        _centerPoint = transform.position;
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

        if(IsMoving)
        {
            if (cam == _gameManager.CameraManager.PlayerFollowCamera)
            {
                if (_slideCoroutine != null)
                {
                    StopCoroutine(_slideCoroutine);
                    StopCoroutine(_slideCoroutine1);
                    
                    //transform.position = _initialPosition;
                }
                return;
            }

            SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();
     
            if (securityCameraComponent != null && securityCameraComponent.IsElectric)
            {   
                if(transform.position==forwardObject.transform.position)
                    _slideCoroutine = StartCoroutine(SlideObject(transform.gameObject, forwardObject.transform.position, backwardObject.transform.position));
                else
                    _slideCoroutine = StartCoroutine(SlideObject(transform.gameObject, backwardObject.transform.position, forwardObject.transform.position));
            }
        }


        if(IsRotating)
        {
            if (cam == _gameManager.CameraManager.PlayerFollowCamera)
            {
                if (_slideCoroutine != null)
                {
                    StopCoroutine(_slideCoroutine);
                    //transform.rotation = _initialRotation;
                    //transform.position = _initialPosition;

                }
                
                return;
            }

            SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();

            if (securityCameraComponent != null && securityCameraComponent.IsElectric)
            {
               
                    _slideCoroutine = StartCoroutine(RotateCoroutine());
                
            }


        }
        
    }

    // Moving Object back and forth
    private IEnumerator SlideObject(GameObject obj, Vector3 start, Vector3 target)
    {
        
        while (true)
        {
             yield return  _slideCoroutine1=StartCoroutine(MoveToPosition(obj, start, target));
             yield return _slideCoroutine1=StartCoroutine(MoveToPosition(obj, target, start));
        }
    }


    private IEnumerator MoveToPosition(GameObject obj, Vector3 start, Vector3 target)
    {
        float t = 0;
        Vector3 startingPos = obj.transform.position;

        while (t < 1)
        {
            t += Time.deltaTime / slideDuration;
            obj.transform.position = Vector3.Lerp(startingPos, target, t);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("On trigger enter");
            other.transform.parent = transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("On trigger exit");
            other.transform.parent = _playerParent;
        }

    }


    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            float currentAngle = 0f;
            float targetAngle = 360;

            while (currentAngle < targetAngle)
            {
                float rotationStep = rotationSpeed * Time.deltaTime;
                //transform.Rotate(Vector3.up, rotationStep);
                transform.RotateAround(_centerPoint, Vector3.up, rotationStep);
                currentAngle += rotationStep;
                
            }
            yield return null;
        }
    }





}
