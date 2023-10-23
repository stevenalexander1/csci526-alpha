using System;
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
    private GameManager _gameManager;
    // Properties
    public Image Crosshair => crosshair;
    public Text GameOverText => gameOverText;
    public Text CashText => cashText;
    public GameObject GameOverPanel => gameOverPanel;
    
    public StealthBar StealthBar => stealthBar;

    void Start()
    {
        gameOverPanel.SetActive(false);
        _gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        if (_gameManager)
        {
            _gameManager.GameOverEvent += HandleGameOver;
            PlayerCharacter playerCharacter = _gameManager.PlayerCharacter;
            if (playerCharacter)
            {
                playerCharacter.StealthMeterChangedEvent += HandleStealthMeterChanged;
                playerCharacter.CashChangedEvent += HandleCashChanged;
            }
        }
    }

   
    private void OnDisable()
    {
        if (_gameManager)
        {
            _gameManager.GameOverEvent -= HandleGameOver;
            PlayerCharacter playerCharacter = _gameManager.PlayerCharacter;
            if (playerCharacter)
            {
                playerCharacter.StealthMeterChangedEvent -= HandleStealthMeterChanged;
                playerCharacter.CashChangedEvent -= HandleCashChanged;
            }
        }
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

    private void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void HandleStealthMeterChanged(float prev, float next)
    {
        stealthBar.SetStealth(Mathf.Clamp(next, 0, stealthBar.StealthSlider.maxValue));
    }
    
    private void HandleCashChanged(int prev, int next)
    {
        cashText.text = "Cash: $" + next + "/1000";
    }

}
