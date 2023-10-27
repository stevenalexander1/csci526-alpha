using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string[] instructions;
    private int i = 0;
    // Start is called before the first frame update
    
    public string GetNextInstruction()
    {
        if (i < instructions.Length)
            return instructions[i++];
        return "";
    }
}
