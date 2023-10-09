using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private GameObject _grabbableGameObject;
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
            _grabbableGameObject = other.gameObject;
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
        GameManager.Instance.UpdateScore(1);
        GameManager.Instance.UpdateCash(grabbableObject.ItemValue);
    }
}