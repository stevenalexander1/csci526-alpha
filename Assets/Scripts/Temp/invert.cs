using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invert : MonoBehaviour
{
    public bool inverter=false;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        _player.transform.Rotate(new Vector3(0, 0, 180));
        inverter = !inverter;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
