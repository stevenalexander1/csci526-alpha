using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    // Delegates
    public delegate void StealthMeterChangedEventDelegate(float prev, float next);

    public StealthMeterChangedEventDelegate StealthMeterChangedEvent;

    public delegate void CashChangedEventDelegate(int prev, int next);

    public CashChangedEventDelegate CashChangedEvent;
    [Header("References")] GameManager gameManager;
    UIManager uiManager;
    [SerializeField] private GameObject _playerFollowCamera;
    [Header("Grabbables")] private GameObject _grabbableGameObject;
    private bool _canGrabObject = false;

    [Header("Player State")] [SerializeField]
    private float maxStealthMeter = 0.25f;
    private float _currentStealthMeter;
    private int _cash = 0;
    private bool _inRangeOfInteractable = false;
    private GameObject interactable = null;
    public GameObject PlayerFollowCamera => _playerFollowCamera;

    public float CurrentStealthMeter
    {
        get => _currentStealthMeter;
        set => _currentStealthMeter = value;
    }

    private void Awake()
    {
    }

    public void Start()
    {
        _currentStealthMeter = maxStealthMeter;
        gameManager = GetComponent<GameManager>();
        uiManager = gameManager.UIManager;
        uiManager.StealthBar.SetMaxStealth(maxStealthMeter);
    }

    public void OnEnable()
    {
        StealthMeterChangedEvent += HandleStealthMeterChanged;
        CashChangedEvent += HandleCashChanged;
    }

    public void OnDisable()
    {
        StealthMeterChangedEvent -= HandleStealthMeterChanged;
        CashChangedEvent -= HandleCashChanged;
    }


    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            gameManager.UIManager.GameOverText.text = "Mission Passed";
            gameManager.LevelCompleteEvent?.Invoke();
        }

        if (other.CompareTag("Message"))
        {
            gameManager.UIManager.ShowInstructionText(gameManager.TutorialManager.GetNextInstruction());
        }

        if (other.CompareTag("HarmfulTerrain"))
        {
            if (gameManager.IsGameOver) return;
            gameManager.GameOver();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the grabber collided with an object that can be grabbed.
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = true;
            _grabbableGameObject = other.gameObject;
        }

        if (other.CompareTag("Laser"))
        {
            if (gameManager.IsGameOver) return;
            ChangeCurrentStealthValue(-Time.deltaTime);
        }

        if (other.GetComponent<DoorController>() != null)
        {
            _inRangeOfInteractable = true;
            interactable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = false;
            _grabbableGameObject = null;
        }

        if (other.CompareTag("FinishLine"))
        {
            if (_cash < 1000)
            {
                gameManager.UIManager.ToggleGameMessageText();
            }
        }

        if (other.CompareTag("Message"))
        {
            gameManager.UIManager.HideInstructionText();
            other.gameObject.SetActive(false);
        }
        if (other.GetComponent<DoorController>() != null)
        {
            _inRangeOfInteractable = false;
            interactable = null;
        }
    }
    
    public void InteractWithDoor()
    {
        if (!_inRangeOfInteractable) return;
        interactable.GetComponent<DoorController>().OpenDoor();
    }

    public void GrabObject()
    {
        if (!_canGrabObject
            || !_grabbableGameObject
            || _grabbableGameObject.GetComponent<GrabbableObject>().IsGrabbed) return;
        GrabbableObject grabbableObject = _grabbableGameObject.GetComponent<GrabbableObject>();
        grabbableObject.IsGrabbed = true;
        _grabbableGameObject.SetActive(false);
        UpdateCash(grabbableObject.ItemValue);
    }

    private void UpdateCash(int cash)
    {
        CashChangedEvent?.Invoke(_cash, _cash + cash);
    }

    public void ChangeCurrentStealthValue(float value)
    {
        StealthMeterChangedEvent?.Invoke(_currentStealthMeter, _currentStealthMeter + value);
    }

    private void HandleStealthMeterChanged(float prev, float next)
    {
        _currentStealthMeter = next;
        if (_currentStealthMeter <= 0)
        {
            gameManager.GameOver();
        }
    }

    private void HandleCashChanged(int prev, int next)
    {
        _cash = next;
    }
}