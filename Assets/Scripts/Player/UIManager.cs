using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace
using Cinemachine;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image crosshair;  
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text cashText;
    [SerializeField] private StealthBar stealthBar;

    [SerializeField] private TextMeshProUGUI GameMessageText;
    [SerializeField] private GameObject moveInstructions;
    // Properties
    public Image Crosshair => crosshair;
    public Text GameOverText => gameOverText;
    public Text CashText => cashText;
    public GameObject GameOverPanel => gameOverPanel;
    
    public StealthBar StealthBar => stealthBar;

    void Start()
    {
        gameOverPanel.SetActive(false);
        

    }

    void Update()
    {
        
    }
    
    public void ToggleCrosshairVisibility()
    {
        crosshair.enabled = !crosshair.enabled;
    }
    
    public void ToggleGameMessageText()
    {
        GameMessageText.enabled = !GameMessageText.enabled;
    }

    public void DisableInstructionText()
    {
        moveInstructions.SetActive(false);
    }
}
