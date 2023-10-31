using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionPlayer : MonoBehaviour
{
    // Add this to the holographic object
    private GameObject _player;
    [SerializeField] private bool repositionX = true;
    [SerializeField] private bool repositionY = true;
    [SerializeField] private bool repositionZ = true;

    private GameManager _gameManager;

    private Vector3 telePostion;
    void Start()
    {
        telePostion = transform.GetChild(0).position;
        _gameManager = GameManager.Instance;
        _player = _gameManager.PlayerCharacter.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Reposition();
        }
    }
    private void Reposition()
    {
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = new Vector3(repositionX ? telePostion.x : _player.transform.position.x,
                                                 repositionY ? telePostion.y : _player.transform.position.y,
                                                 repositionZ ? telePostion.z : _player.transform.position.z);
        _player.GetComponent<CharacterController>().enabled = true;
    }

}
