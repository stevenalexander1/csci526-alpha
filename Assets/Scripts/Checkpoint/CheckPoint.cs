using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static HashSet<string> checkpointList = new HashSet<string>();
    private string _name;
  
    private void Awake()
    {
        _name = transform.gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player") && !checkpointList.Contains(_name))
        {
            GameManager.lastCheckpoint = transform.position;
            checkpointList.Add(_name);

        }
    }
}
