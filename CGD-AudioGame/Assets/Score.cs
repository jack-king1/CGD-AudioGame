using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //We need this script to store the score because the player is deleted upon death.
    private float finalScore = 0;
    public float playerScore = 0;

    public float CalculateScore(bool won, float gameTimer)
    {
        if(won)
        {
            finalScore = playerScore * gameTimer;
        }
        else
        {
            finalScore = playerScore;
        }

        return finalScore;
    }
}
