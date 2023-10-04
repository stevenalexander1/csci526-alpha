using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string levelName;
    [SerializeField] private int levelIndex;
    [SerializeField] private int minimumLevelCash;
    [SerializeField] private int maximumLevelCash;

    [SerializeField] private List<GrabbableObject> grabbableObjects;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
