using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabberObject : MonoBehaviour
{
    private GameObject grabbedObject;
    private Rigidbody objectRigidbody;
    public Text scoreText;

    

    public void Start()
    {
       
        scoreText.text = "Items: " + ScoreManager.score+"/4";
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the grabber collided with an object that can be grabbed.
        if (other.CompareTag("Grabbable"))
        {
            GrabObject(other.gameObject);
        }

    }

   
    private void GrabObject(GameObject objToGrab)
    {
        grabbedObject = objToGrab;
        objectRigidbody = grabbedObject.GetComponent<Rigidbody>();
        // Disable the object's physics so that it doesn't fall or react to forces.
        objectRigidbody.isKinematic = true;
        grabbedObject.SetActive(false);
        ScoreManager.IncreaseScore(1);
        scoreText.text = "Items: " + ScoreManager.score+"/4";
      
    }

    
}
