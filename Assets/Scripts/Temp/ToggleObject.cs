using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    private CameraManager cam;
    private GameObject _mainCamera;
    private GameObject wall;
    private bool overlap = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        wall = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wall.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            wall.GetComponent<Collider>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraManager cameraManager = _mainCamera.GetComponent<CameraManager>();
        if (!cameraManager.PlayerCameraActive)
        {
            wall.SetActive(true);

        }
        else
        {
            wall.SetActive(false);
        }
    }
}
