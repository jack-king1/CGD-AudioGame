using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    public float playerScore = 0;

    public int PlayerID()
    {
        return playerID;
    }

    public float PlayerScore()
    {
        return playerScore;
    }

}
