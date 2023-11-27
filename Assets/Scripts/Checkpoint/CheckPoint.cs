using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static HashSet<string> checkpointList = new HashSet<string>();
    private string _name;
    private static bool _flag=false;
    private GameManager _gameManager;

    [SerializeField]
    private Quaternion customRotation = Quaternion.identity;
    public Quaternion CustomRotation => customRotation;

    private void Awake()
    {
        _name = transform.gameObject.name;
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player") && !checkpointList.Contains(_name))
        {
            GameManager.lastCheckpoint = transform.position;
            GameManager.checkpointRotation = customRotation;
            checkpointList.Add(_name);
            _flag = true;
            displayCheckPoint();

        }
    }

    private void displayCheckPoint()
    {
        // _gameManager.UIManager.showCheckPoint("Checkpoint Reached");
        _gameManager.UIManager.ShowCheckpointInstruction("CHECKPOINT REACHED");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _flag)
        {
           // Debug.Log("In trigger exit");
            _flag = false;
            // _gameManager.UIManager.HideCheckPointText();
            _gameManager.UIManager.HideCheckpointInstruction();
        }


    }

}
