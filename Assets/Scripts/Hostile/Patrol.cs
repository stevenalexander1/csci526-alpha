using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int index = 1;
    private int increment = 1;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        transform.LookAt(moveSpots[index]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[index].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[index].position)<0.2f)
        {
            if (waitTime <= 0)
            {
                if (index == 0 || index == moveSpots.Length - 1)
                    increment *= -1;
                index += increment;
                transform.LookAt(moveSpots[index]);
                waitTime = startWaitTime;

            }
            else
                waitTime -= Time.deltaTime;
        }
    }
}
