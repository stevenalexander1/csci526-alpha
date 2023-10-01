using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Static variable to hold the score
    public static int score = 0;

    // This function increases the score
    public static void IncreaseScore(int points)
    {
        score += points;
    }
}
