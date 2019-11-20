using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int playerID;
    private float playerScore;

    public int PlayerID()
    {
        return playerID;
    }

    public float PlayerScore()
    {
        return playerScore;
    }

}
