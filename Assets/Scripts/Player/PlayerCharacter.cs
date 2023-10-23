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
    [Header("References")]
    GameManager gameManager;
    UIManager uiManager;
    [Header("Grabbables")]
    private GameObject _grabbableGameObject;
    private bool _canGrabObject = false;

    [Header("Player State")] 
    [SerializeField]
    private float maxStealthMeter = 0.25f;
    private float currentStealthMeter;
    private int _cash = 0;

    public float CurrentStealthMeter
    {
        get => currentStealthMeter;
        set => currentStealthMeter = value;
    }

    private void Awake()
    {
    }

    public void Start()
    {
        currentStealthMeter = maxStealthMeter;
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
        if (other.CompareTag("FinishLine")) {
            if (_cash >= 1000)
            {
                gameManager.UIManager.GameOverText.text = "Mission Passed";
                gameManager.GameOver();
            }
            else 
            {
                gameManager.UIManager.ToggleGameMessageText();
            }
        }
        if (other.CompareTag("Message")) gameManager.UIManager.DisableInstructionText();
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = false;
            _grabbableGameObject = null;
        }  
        if (other.CompareTag("FinishLine")) {
            if (_cash < 1000)
            {
                gameManager.UIManager.ToggleGameMessageText();
            }
        }
    }

    public void GrabObject()
    {
        Debug.Log("Grabbing object");
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
        StealthMeterChangedEvent?.Invoke(currentStealthMeter, currentStealthMeter + value);
    }
    
    private void HandleStealthMeterChanged(float prev, float next)
    {
        currentStealthMeter = next;
        Debug.Log("Stealth meter: " + currentStealthMeter);
        if (currentStealthMeter <= 0)
        {
            gameManager.GameOver();
        }
    }
    
    private void HandleCashChanged(int prev, int next)
    {
        _cash = next;
        Debug.Log("Cash: " + _cash);
        
    }

}