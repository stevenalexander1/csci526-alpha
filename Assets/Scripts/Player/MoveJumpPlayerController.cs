using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpPlayerController : MonoBehaviour
{
    private Transform player;
    public GameObject playerObject;
    public Text instruction;
    public void Start()
    {
        player = playerObject.transform;
    }

    private void Update()
    {
        if(player.position.y > 1)
        {
            instruction.text = "";
        }
    }

}