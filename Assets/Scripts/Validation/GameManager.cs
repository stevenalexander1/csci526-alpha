using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    //private bool isGameOver = false;
    public Text gameOverText;
    [SerializeField]
    private FirstPersonController fpsController;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Game Started!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine")) {
            // isGameOver = true;
            //Time.timeScale = 0;
            if (ScoreManager.score == 4)
            {
                gameOverText.text ="Mission Passed";
            }
            else
            {
                gameOverText.text ="Mission Failed";
            }
            gameOverCanvas.SetActive(true);
            GameOver();
            
    
        }
    }


    public void GameOver()
    {
        fpsController.gameOver = true;
        Debug.Log("Game Over!");
        // Display the "GAME OVER" text
        gameOverText.gameObject.SetActive(true);
        //Time.timeScale = 0;
        // Enable mouse
        Cursor.lockState = CursorLockMode.Confined;
        // You can also add other game over logic here, like pausing the game or showing a restart button.
    }
    
 
    
    public void RestartGame()
    {
        fpsController.gameOver = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
          Time.timeScale = 1;
          gameOverCanvas.SetActive(false);
          ScoreManager.score = 0;
          Cursor.lockState = CursorLockMode.Locked;
    



    }


}
