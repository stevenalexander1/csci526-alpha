using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Vector3 lastCheckpoint = Vector3.zero;
    public static Quaternion checkpointRotation = Quaternion.identity;
    
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
    [SerializeField] public TMP_Text _lvlName;


    public TMP_Text _ctext;

    private TMP_Text _lname=>_lvlName;
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
    {   //Checkpoint code:
        if(lastCheckpoint!=Vector3.zero)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpoint;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = checkpointRotation;
            Debug.Log("Latest checkpoint set");
        }

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
        lastCheckpoint = Vector3.zero;
        //CheckPoint.checkpointList = new HashSet<string>();
        CheckPoint.checkpointList.Clear();
        checkpointRotation=Quaternion.identity;

        if (_currentLevelIndex >= levels.Count)
        {
            Debug.Log("No more levels to load!");
            return;
        }
        //SceneManager.LoadScene(levelManager.currentLevel.NextLevelSceneName);
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            // Send Analytics:
            if (SendToGoogle.prevSessionID != SendToGoogle.getSessionId())
            {

                gameObject.GetComponent<AnalyticsManager>().Send();
                SendToGoogle.prevSessionID = SendToGoogle.getSessionId();
                SendToGoogle.resetParameters();
            }

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
        uiManager.StealthBar.StealthSlider.value = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void HandleLevelComplete()
    {
        lastCheckpoint = Vector3.zero;
        //CheckPoint.checkpointList = new HashSet<string>();
        CheckPoint.checkpointList.Clear();
        checkpointRotation = Quaternion.identity;

        _isGameOver = true;
        Debug.Log("Level Complete!");
        Cursor.lockState = CursorLockMode.Confined;

        // Analytics 2:
        if(SendToGoogle.getPlayerDieCount()>3)
        {
            SendToGoogle.setPlayerFailLevels(SceneManager.GetActiveScene().name);
            //Debug.Log("Setting failed level"+ SendToGoogle.getPlayerFailLevels());
        }
        else
        {
            SendToGoogle.setPlayerPassLevels(SceneManager.GetActiveScene().name);
            //Debug.Log("Setting Passed level" + SendToGoogle.getPlayerPassLevels());
        }
        SendToGoogle.setPlayerDieCount(-SendToGoogle.getPlayerDieCount());
        //Debug.Log("Reset Die Count"+SendToGoogle.getPlayerDieCount());

        //Analytics 3: Player vs Laser
        if(SendToGoogle.laserExists)
        {
            if (SendToGoogle.getIsLaserDeath())
            {
                SendToGoogle.setLaserFailLevels(SceneManager.GetActiveScene().name);
                //Debug.Log("Fail laser level" + SendToGoogle.getLaserFailLevels());
            }
            else
            {
                SendToGoogle.setLaserPassLevels(SceneManager.GetActiveScene().name);
                //Debug.Log("Pass laser level" + SendToGoogle.getLaserPassLevels());
            }
            SendToGoogle.setIsLaserDeath(false);
            SendToGoogle.laserExists = false;
        }
        
        //Analytics 4: Player vs Guard
        if(SendToGoogle.guardExists)
        {
            if (SendToGoogle.getIsGuardDeath())
            {
                SendToGoogle.setGuardFailLevels(SceneManager.GetActiveScene().name);
                //Debug.Log("Fail guard level" + SendToGoogle.getGuardFailLevels());
            }
            else
            {
                SendToGoogle.setGuardPassLevels(SceneManager.GetActiveScene().name);
                //Debug.Log("Pass guard level" + SendToGoogle.getGuardPassLevels());
            }
            SendToGoogle.setIsGuardDeath(false);
            SendToGoogle.guardExists = false;

        }
        


    }
    
    
    public void PopInGameMenu()
    {
        if (_isGameOver) return;
        PauseGameEvent?.Invoke();
        _isPaused = true;
        Scene currScene = SceneManager.GetActiveScene();
        _lname.text = (currScene.buildIndex-1).ToString()+". "+currScene.name.ToString();
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
        lastCheckpoint = Vector3.zero;
        //CheckPoint.checkpointList = new HashSet<string>();
        CheckPoint.checkpointList.Clear();
        checkpointRotation = Quaternion.identity;

        _isGameOver = false;
        _isPaused = false;
        SceneManager.LoadScene("BetaMainMenu"); 
        Time.timeScale = 1;

        // Send analytics
        Debug.Log("Quit game");
        if(SendToGoogle.prevSessionID!=SendToGoogle.getSessionId())
        {
           
            gameObject.GetComponent<AnalyticsManager>().Send();
            SendToGoogle.prevSessionID = SendToGoogle.getSessionId();
            SendToGoogle.resetParameters();
        }
    }

    public void ButtonRestart()
    {
        lastCheckpoint = Vector3.zero;
        //CheckPoint.checkpointList = new HashSet<string>();
        CheckPoint.checkpointList.Clear();
        checkpointRotation = Quaternion.identity;
        RestartGame();
    }
}
