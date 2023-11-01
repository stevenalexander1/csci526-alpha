using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private static int _currentLevelIndex = 0;
    
    // Delegates
    public delegate void GameOverEventDelegate();

    public GameOverEventDelegate GameOverEvent;
    
    public delegate void LevelCompleteEventDelegate();
    
    public LevelCompleteEventDelegate LevelCompleteEvent;
    
    public delegate void PauseGameEventDelegate();
    
    public PauseGameEventDelegate PauseGameEvent;
    
    [Header("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private LevelManager levelManager;
    private GameObject _mainCamera;
    [Header("Level")] 
    [SerializeField] private List<Level> levels;
    
    [Header("Game State")]
    private bool _isGameOver = false; // Track game over state
    private bool _isPaused = false; // Track pause state
    public bool IsGameOver => _isGameOver;
    
    public bool IsPaused => _isPaused;

    public UIManager UIManager => uiManager;
    
    public PlayerCharacter PlayerCharacter => playerCharacter;
    public GravityManager GravityManager => gravityManager;
    
    public CameraManager CameraManager => cameraManager;

    public TutorialManager TutorialManager => tutorialManager;
    
    public LevelManager LevelManager => levelManager;

    private void Awake()
    {
    // Make sure there is only one instance of this object
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Cursor.lockState = CursorLockMode.Locked;    
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            cameraManager = _mainCamera.GetComponent<CameraManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
    }

    private void OnEnable()
    {
        GameOverEvent += HandleGameOver;
        LevelCompleteEvent += HandleLevelComplete;
         
    }
    
    private void OnDisable()
    {
        GameOverEvent -= HandleGameOver;
        LevelCompleteEvent -= HandleLevelComplete;
    }
    

    public void GameOver()
    {
        if (_isGameOver) return;
        GameOverEvent?.Invoke();
    }
    
    public void LoadNextLevel()
    {
        if (_currentLevelIndex >= levels.Count)
        {
            Debug.Log("No more levels to load!");
            return;
        }
        //SceneManager.LoadScene(levelManager.currentLevel.NextLevelSceneName);
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("No more levels to load!");
            // Load main menu
            SceneManager.LoadScene("BetaMainMenu");
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
 
    
    public void RestartGame()
    {
        //if (!isGameOver) return;
        _isGameOver = false;
        _isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        uiManager.GameOverPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void HandlePauseGame()
    {
        
    }
    private void HandleGameOver()
    {
        _isGameOver = true;
        Debug.Log("Game Over!");
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void HandleLevelComplete()
    {
        _isGameOver = true;
        Debug.Log("Level Complete!");
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    
    public void PopInGameMenu()
    {
        if (_isGameOver) return;
        PauseGameEvent?.Invoke();
        _isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        _isGameOver = false;
        _isPaused = false;
        uiManager.InGamePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        _isGameOver = false;
        _isPaused = false;
        SceneManager.LoadScene("BetaMainMenu"); 
        Time.timeScale = 1;
    }
}
