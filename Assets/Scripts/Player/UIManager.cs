using System;
using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image crosshair;  
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text cashText;
    [SerializeField] private StealthBar stealthBar;

    [SerializeField] private TextMeshProUGUI GameMessageText;
    [SerializeField] private TextMeshProUGUI instructions;
    private GameManager _gameManager;
    private CameraManager _cameraManager;
    // Properties
    public Image Crosshair => crosshair;
    public Text GameOverText => gameOverText;
    public Text CashText => cashText;
    public GameObject GameOverPanel => gameOverPanel;
    
    public StealthBar StealthBar => stealthBar;

    public TextMeshProUGUI Instructions => instructions;

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
            _cameraManager = _gameManager.CameraManager;
            if (_cameraManager)
            {
                _cameraManager.CameraChangedEvent += HandleCameraChangedEvent;
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
    
    public void SetCrossHairVisibility(bool visible)
    {
        crosshair.enabled = visible;
    }
    
    public void ToggleGameMessageText()
    {
        GameMessageText.enabled = !GameMessageText.enabled;
    }

    public void ShowInstructionText(string s)
    {
        instructions.text = s;
        StartCoroutine(FadeTextToFullAlpha(0.15f, instructions));
    }

    public void HideInstructionText()
    {
        StartCoroutine(FadeTextToZeroAlpha(0.15f, instructions));
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
    
    private void HandleCameraChangedEvent(GameObject camera)
    {
        if (camera == null) return;
        SetCrossHairVisibility(_cameraManager.PlayerCameraActive);
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
