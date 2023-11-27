using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   // private static bool _setFlag=false;
    public static HashSet<string> checkpointList = new HashSet<string>();
    private string _name;
   Material material;
   


    private void Awake()
    {
      //  Renderer renderer = GetComponent<Renderer>();
      //  material = renderer.material;
        name = transform.gameObject.name;

      //  if (checkpointList.Contains(name) && renderer!=null)
      //  {
      //      material.color = Color.green;
       // }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player") && !checkpointList.Contains(name))
        {
            //Debug.Log("In Player");
            GameManager.lastCheckpoint = transform.position;

           /*Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                material.color = Color.green;
                Debug.Log("In Renderer");
            }
           */
            checkpointList.Add(name);

        }
    }
}
