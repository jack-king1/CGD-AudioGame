using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    public float playerScore = 0;
    public float finalScore = 0;

    public int PlayerID()
    {
        return playerID;
    }

    public float PlayerScore()
    {
        return playerScore;
    }

    public void PlayerScore(float amount)
    {
        playerScore += amount;
    }

    public float CalculateFinalScore(bool levelWon)
    {
        if(levelWon)
        {
            finalScore = playerScore * GameObject.FindGameObjectWithTag("LevelManager").
                GetComponent<LevelManager>().LevelTimer();
        }
        else
        {
            finalScore = playerScore;
        }
        return finalScore;
    }
}
