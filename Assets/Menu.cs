using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject difficultyPanel;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    public GameObject ctrlPanel => controlsPanel;
    public GameObject diffPanel => difficultyPanel;
    private string level;
    private string difficulty;
    private Hashtable diff_map = new Hashtable();
    private Hashtable tut_map = new Hashtable();
    private Hashtable easy_map = new Hashtable();
    private Hashtable med_map = new Hashtable();
    private Hashtable hard_map = new Hashtable();
    private string scene_name;
    private Hashtable map_name;
    public Button tut_button => tutorialButton;
    public Button easy_button => easyButton;
    public Button med_button => mediumButton;
    public Button hard_button => hardButton;

    public void Start()
    {
        ctrlPanel.SetActive(false);
        diffPanel.SetActive(false);
        tut_button.gameObject.SetActive(true);
        easy_button.gameObject.SetActive(true);
        med_button.gameObject.SetActive(true);
        hard_button.gameObject.SetActive(true);
        level = "";
        difficulty = "";
        // difficulty level to scene mapping
        diff_map.Add("tutorial", tut_map);
        diff_map.Add("easy", easy_map);
        diff_map.Add("medium", med_map);
        diff_map.Add("hard", hard_map);
        // tutorial
        tut_map.Add("movement", "MoveTutorial");
        tut_map.Add("camera", "CameraTutorial");
        tut_map.Add("gravity", "");
        tut_map.Add("laser", "");
        tut_map.Add("guard", "");
        tut_map.Add("hologram", "");
        // easy
        easy_map.Add("movement", "MoveSection2");
        easy_map.Add("camera", "");
        easy_map.Add("gravity", "GravitySection");
        easy_map.Add("laser", "");
        easy_map.Add("guard", "");
        easy_map.Add("hologram", "");
        // medium
        med_map.Add("movement", "");
        med_map.Add("camera", "");
        med_map.Add("gravity", "");
        med_map.Add("laser", "");
        med_map.Add("guard", "");
        med_map.Add("hologram", "");
        // hard
        hard_map.Add("movement", "");
        hard_map.Add("camera", "");
        hard_map.Add("gravity", "");
        hard_map.Add("laser", "");
        hard_map.Add("guard", "");
        hard_map.Add("hologram", "");
        scene_name = "";
    }

    private void activate_valid_buttons()
    {
        if (tut_map[level].ToString().Length > 0)
        {
            tut_button.gameObject.SetActive(true);
        }
        else
        {
            tut_button.gameObject.SetActive(false);
        }
        if (easy_map[level].ToString().Length > 0)
        {
            easy_button.gameObject.SetActive(true);
        }
        else
        {
            easy_button.gameObject.SetActive(false);
        }
        if (med_map[level].ToString().Length > 0)
        {
            med_button.gameObject.SetActive(true);
        }
        else
        {
            med_button.gameObject.SetActive(false);
        }
        if (hard_map[level].ToString().Length > 0)
        {
            hard_button.gameObject.SetActive(true);
        }
        else
        {
            hard_button.gameObject.SetActive(false);
        }
    }

    public void OnMovementButton() {
        level = "movement";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnCameraButton()
    {
        level = "camera";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnGravityButton()
    {
        level = "gravity";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnLaserButton()
    {
        level = "laser";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnGuardButton()
    {
        level = "guard";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnHologramButton()
    {
        level = "hologram";
        activate_valid_buttons();
        diffPanel.SetActive(true);
    }

    public void OnCloseButton()
    {
        diffPanel.SetActive(false);
        level = "";
        difficulty = "";
        scene_name = "";
    }

    public void OnTutorial()
    {
        difficulty = "tutorial";
        load_scene();
    }

    public void OnEasy()
    {
        difficulty = "easy";
        load_scene();
    }

    public void OnMedium()
    {
        difficulty = "medium";
        load_scene();
    }

    public void OnHard()
    {
        difficulty = "hard";
        load_scene();
    }

    private void load_scene()
    {
        map_name = (Hashtable)diff_map[difficulty];
        scene_name = (string)map_name[level];
        if (scene_name.Length > 0)
        {
            SceneManager.LoadScene(scene_name);
        }
    }

    public void ShowControlPanel()
    {
        ctrlPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        ctrlPanel.SetActive(false);
    }

}
