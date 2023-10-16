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
    [SerializeField] private FirstPersonController fpsController;
        
    [Header("Canvas References")]
    [SerializeField] private UIManager uiManager;
    public GameObject gameOverCanvas;
    public Text gameOverText;
    public GameObject panel;


    [Header("Score")]
    public Text cashText;

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
        // gameOverText.gameObject.SetActive(false);
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void GameOver()
    {
        Debug.Log("Game Over!");
        panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        isGameOver = true;
    }
    
 
    
    public void RestartGame()
    {
        isGameOver = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); 
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    

}
