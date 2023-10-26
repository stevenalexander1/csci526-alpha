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

    // Delegates
    public delegate void GameOverEventDelegate();

    public GameOverEventDelegate GameOverEvent;
    
    [Header("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private GravityManager gravityManager;
    [SerializeField] private CameraManager cameraManager;
    private GameObject _mainCamera;
    [Header("Level")] 
    [SerializeField] private List<Level> levels;
    private Level currentLevel;
    private int currentLevelIndex = 0;
    
    [Header("Game State")]
    private bool isGameOver = false; // Track game over state
    
    public bool IsGameOver => isGameOver;
    public UIManager UIManager => uiManager;
    
    public PlayerCharacter PlayerCharacter => playerCharacter;
    public GravityManager GravityManager => gravityManager;
    
    public CameraManager CameraManager => cameraManager;

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
        
    }

    private void OnEnable()
    {
        GameOverEvent += HandleGameOver;
    }
    
    private void OnDisable()
    {
        GameOverEvent -= HandleGameOver;
    }
    

    public void GameOver()
    {
        if (isGameOver) return;
        GameOverEvent?.Invoke();
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
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    

    

}
