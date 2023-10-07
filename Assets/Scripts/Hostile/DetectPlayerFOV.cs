using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerFOV : MonoBehaviour
{
    [SerializeField]
    private float range;
    [SerializeField] [Range(0,360)]
    private float angle;

    public GameObject player;
    public LayerMask targetMask; // Contains the player's layer
    public LayerMask obstructionMask; // Contains any obstruction layer

    [SerializeField]
    private GameManager gameManager;
    private bool canSeePlayer;

    private bool isGameOver = false; // Track game over state

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DetectFOV());
    }

    private IEnumerator DetectFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FOVCheck();
        }
    }

    private void FOVCheck()
    {
        if(isGameOver)
        {
            return;
        }

        Collider[] rangeCheck = new Collider[1];
        if (Physics.OverlapSphereNonAlloc(transform.position, range, rangeCheck, targetMask) == 1) // Checks if there is an object with targetMask in given radius
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) // Must be within the given angle
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) // Check if a raycast is not hitting an obstruction
                {
                    canSeePlayer = true;
                    gameManager.GameOver();
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if(canSeePlayer)
            canSeePlayer = false;

        //if (canSeePlayer)
        //    Debug.Log("Seen");
        //else
        //    Debug.Log("Unseen");

    }
}
