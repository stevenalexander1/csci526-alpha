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
    private bool isGameOver = false; // Track game over state
    
    public bool IsGameOver => isGameOver;
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
        if (isGameOver) return;
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
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
 
    
    public void RestartGame()
    {
        if (!isGameOver) return;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        uiManager.GameOverPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void HandleGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        //Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void HandleLevelComplete()
    {
        isGameOver = true;
        Debug.Log("Level Complete!");
        //Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    
    public void PopInGameMenu()
    {
        if (isGameOver) return;
        uiManager.InGamePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        isGameOver = false;
        uiManager.InGamePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        isGameOver = false;
        SceneManager.LoadScene("BetaMainMenu"); 
        Time.timeScale = 1;
    }
}
