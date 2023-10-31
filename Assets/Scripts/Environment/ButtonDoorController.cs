using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script for handling Door Button in CameraEasyLevel

public class ButtonDoorController : MonoBehaviour
{
    private GameObject _mainCamera;
    private float _distanceThreshold = 6f;
    private Camera _playerCamera;
    private int _count = 0;
    private DoorController _doorController;



    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _playerCamera = _mainCamera.GetComponent<Camera>();
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
           if(keyboard.eKey.wasPressedThisFrame)
            { 
                RaycastHit hit;
                Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                if (_count<1 && Physics.Raycast(ray, out hit) && hit.transform.CompareTag("DoorButton") && Vector3.Distance(transform.position, hit.transform.position) < _distanceThreshold)
                {
                    if (hit.transform.parent != null)
                    {
                        GameObject sibling = hit.transform.parent.Find("Door").gameObject;
                        _doorController = sibling.GetComponent<DoorController>();
                        _doorController.OpenDoor();
                        
                    }
                    
                    _count++;
                   
                }


            }

           

        }       
    }
}
