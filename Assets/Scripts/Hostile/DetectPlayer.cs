using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField]
    private float fieldOfViewAngle;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float viewDistance;
    private RaycastHit[] _raycastHits;
    [SerializeField]
    private int layerMask;

    private bool IsWithinSights(Transform targetTransform)
    {
        var enemyPosition = transform.position;
        var playerPosition = targetTransform.position;


        Vector3 directionToTarget = (playerPosition - enemyPosition);
        if (Mathf.Abs(directionToTarget.y) > 4.0f)
        {
            return false;
        }

        directionToTarget.y = 0f;

        float distance = directionToTarget.magnitude;

        directionToTarget.Normalize();

        if ((Vector3.Angle(directionToTarget, transform.forward) + fieldOfViewAngle / 2) <= fieldOfViewAngle && distance <= viewDistance)
        {
            return Physics.RaycastNonAlloc(enemyPosition, directionToTarget, _raycastHits, viewDistance, layerMask) == 0;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWithinSights(player.transform))
        {
            Debug.Log("Seen");
        }
        else
        {
            Debug.Log("Unseen");
        }
    }
}
