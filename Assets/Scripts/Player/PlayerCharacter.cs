using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("References")]
    GameManager gameManager;
    
    [Header("Grabbables")]
    private GameObject _grabbableGameObject;
    private bool _canGrabObject = false;

    [Header("Player State")] 
    [SerializeField]
    private float maxStealthMeter = 0.25f;
    private float currentStealthMeter;
    [SerializeField]
    private StealthBar stealthBar;
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
        stealthBar.SetMaxStealth(maxStealthMeter);

    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            currentStealthMeter -= Time.deltaTime;
            stealthBar.SetStealth(currentStealthMeter);
            Debug.Log("Stealth meter: " + currentStealthMeter);
            if (currentStealthMeter <= 0)
            {
                gameManager.GameOver();
            }
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
            currentStealthMeter -= Time.deltaTime;
            Debug.Log("Stealth meter: " + currentStealthMeter);
            if (currentStealthMeter <= 0)
            {
                gameManager.GameOver();
                currentStealthMeter = maxStealthMeter;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = false;
            _grabbableGameObject = null;
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
        GameManager.Instance.UpdateCash(grabbableObject.ItemValue);
    }
}