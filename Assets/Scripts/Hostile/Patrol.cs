using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float startWaitTime;
    private float waitTime;
    public Transform locations;

    private List<Transform> moveSpots = new List<Transform>();
    private int index = 0; // Start at the first point
    private int increment = 1;

    // Variables for smooth rotation
    [SerializeField]
    private float rotationSpeed = 2.0f; // Adjust the rotation speed as needed

    // Variable to track whether the object is currently waiting
    private bool isWaiting = false;

    private GameManager _gameManager;
    private SecurityCameraComponent _securityCamera;
   
    [SerializeField] private bool isElectricCam=false;
    public bool IsElectricCam => isElectricCam;
    private Vector3 _initialPosition;
    private Vector3 _initialParentPosition;
    private bool isPositionSet = false;

    private void Awake()
    {
        if(!isPositionSet)
        {
            _initialPosition = transform.position;
            _initialParentPosition = transform.parent.position;
            
        }
           
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        foreach (Transform child in locations)
        {
            moveSpots.Add(child); // Adding patrol locations to the list
        }
        waitTime = startWaitTime;
        if (moveSpots.Count == 0)
            return;
    }

    void Update()
    {
            if (moveSpots.Count == 0)
              return;

            if (!isWaiting)
            {
                // Calculate the direction to the next patrol point
                Vector3 targetDirection = moveSpots[index].position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            if (Vector3.Distance(transform.position, moveSpots[index].position) < 0.2f)
            {
                if (!isWaiting)
                {
                    // Start waiting when reaching the patrol point
                    isWaiting = true;
                }
                else
                {
                    // Handle waiting logic
                    waitTime -= Time.deltaTime;
                    if (waitTime <= 0)
                    {
                        // Stop waiting and continue patrolling
                        isWaiting = false;
                        index = (index + increment) % moveSpots.Count; // Cycle through the points
                        if (index < 0)
                            index = moveSpots.Count - 1;
                        waitTime = startWaitTime;
                    }
                }
            }

            // Move towards the patrol point
            if (!isWaiting)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);
            }
        
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
          if(IsElectricCam)
          {
              if (cam == _gameManager.CameraManager.PlayerFollowCamera)
              {
                speed = 0;

               // transform.parent.position = _initialParentPosition;
              //  transform.position = _initialPosition;
                return;
              }

              SecurityCameraComponent securityCameraComponent = cam.GetComponentInParent<SecurityCameraComponent>();
              if (securityCameraComponent != null && securityCameraComponent.IsElectric)
              {
              
               speed = 2;

            }
              else
              {
              // speed = 2;
            }

          }
  

    }
}
