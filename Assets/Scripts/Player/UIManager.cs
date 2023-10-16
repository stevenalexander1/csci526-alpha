using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace
using Cinemachine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image crosshair;  

    
    // Properties
    public Image Crosshair => crosshair;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void ToggleCrosshairVisibility()
    {
        crosshair.enabled = !crosshair.enabled;
    }
}
