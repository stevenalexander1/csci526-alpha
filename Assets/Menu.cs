using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPlayButton() => SceneManager.LoadScene("VinuthaInGameMenu");

    public void OnLevelButton()
    {
    }

}
