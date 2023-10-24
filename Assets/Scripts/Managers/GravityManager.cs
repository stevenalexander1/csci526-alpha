using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isGravityInverted = false;
    
    public bool IsGravityInverted
    {
        get => _isGravityInverted;
        set => _isGravityInverted = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InvertGravity()
    {
        _isGravityInverted = !_isGravityInverted;
        _gameManager.PlayerCharacter.gameObject.transform.Rotate(new Vector3(0, 0, 180));

    }
}
