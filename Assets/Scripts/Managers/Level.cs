using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private string sceneName;
    [SerializeField] private int levelIndex;
    [SerializeField] private bool isUnlocked;
    public string SceneName => sceneName;
    
   
}
