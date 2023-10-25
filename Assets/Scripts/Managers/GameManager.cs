using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [Header("References")]
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private UIManager uiManager;
   

    [Header("Level")] 
    [SerializeField] private List<Level> levels;
    private Level currentLevel;
    private int currentLevelIndex = 0;
    
    [Header("Game State")]
    private bool isGameOver = false; // Track game over state
    
    public bool IsGameOver => isGameOver;
    public UIManager UIManager => uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // Make sure there is only one instance of this object
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Game Over!");
        uiManager.GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
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

    public void popInGameMenu()
    {
        if (!isGameOver)
        {
            uiManager.InGamePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void resumeGame()
    {
        isGameOver = false;
        uiManager.InGamePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
