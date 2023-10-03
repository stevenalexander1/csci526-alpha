using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private GameObject _grabbableObject;
    private bool _canGrabObject = false;
    public void Start()
    {
       
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the grabber collided with an object that can be grabbed.
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = true;
            _grabbableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            _canGrabObject = false;
            _grabbableObject = null;
        }    
    }

    public void GrabObject()
    {
        if (!_canGrabObject) return;
        Rigidbody grabbedObjectRigidbody = _grabbableObject.GetComponent<Rigidbody>();
        // Disable the object's physics so that it doesn't fall or react to forces.
        grabbedObjectRigidbody.isKinematic = true;
        _grabbableObject.SetActive(false);
        GameManager.Instance.UpdateScore(1);
    }
}