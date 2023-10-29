using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisableCamInst : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
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
        if (cam != _gameManager.CameraManager.PlayerFollowCamera) gameObject.GetComponent<TMP_Text>().text = "";
    }
}
