using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public GameObject _player;
    private GameObject pos;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        pos = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _player.transform.GetComponent<CharacterController>().enabled = false;
            _player.transform.position = new Vector3(pos.transform.position.x, _player.transform.position.y,pos.transform.position.z);
            _player.transform.GetComponent<CharacterController>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
