using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    bool startNextRotation = true;
    public bool rotRight;
    public float yaw;
   

   public float secondsToRot;
    void Start()
    {
        //  StartCoroutine(Rotate(yaw, secondsToRot));
    }

    private void Update(){
        if(startNextRotation && rotRight){
             StartCoroutine(Rotate(yaw, secondsToRot));
        }
        else if (startNextRotation && !rotRight){

              StartCoroutine(Rotate(- yaw, secondsToRot));
        }
    }
    IEnumerator Rotate(float yaw, float duration){
        startNextRotation = false; 
        Quaternion initialRotation = transform.rotation; 
        float timer = 0f;
        while(timer < duration){
            timer += Time.deltaTime;
            transform.rotation =  initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.down );
            yield return null;
        }
        startNextRotation = true; 
        rotRight = !rotRight;

    }
}
