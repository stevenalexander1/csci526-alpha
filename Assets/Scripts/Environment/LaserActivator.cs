using UnityEngine;
using System.Collections.Generic;

public class LaserActivator : MonoBehaviour
{
    // A list to keep track of sibling "Laser" objects.
    private List<GameObject> laserObjects;

    void Start()
    {
        // Initialize the list of sibling "Laser" objects from the parent's siblings.
        laserObjects = new List<GameObject>();

        // Get the parent transform.
        Transform parent = transform.parent;

        // Iterate through the parent's siblings.
        foreach (Transform sibling in parent)
        {
            if (sibling.name == "Laser")
            {
                laserObjects.Add(sibling.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger zone has the tag "Player" (or any other appropriate tag).
        if (other.CompareTag("Player"))
        {
            // Activate the sibling "Laser" objects.
            foreach (var laserObject in laserObjects)
            {
                laserObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the GameObject exiting the trigger zone has the tag "Player" (or any other appropriate tag).
        if (other.CompareTag("Player"))
        {
            // Deactivate the sibling "Laser" objects.
            foreach (var laserObject in laserObjects)
            {
                laserObject.SetActive(false);
            }
        }
    }
}
