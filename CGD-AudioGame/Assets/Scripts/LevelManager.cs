using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float m_levelTimer = 60;
    [SerializeField] private int m_levelID = 1;
    public bool m_startLevel = false;
    private bool levelWon = false;
    private bool levelLost = false;

    private void Update()
    {
        if (m_startLevel)
        {
            if(m_levelTimer > 0)
            {
                m_levelTimer -= Time.deltaTime;
            }
            else if(m_levelTimer <= 0)
            {
                levelLost = true;
                m_levelTimer = 0.0f;
            }
            
        }
    }

    public float LevelTimer()
    {
        return m_levelTimer;
    }

    public bool IsLevelWon()
    {
        return levelWon;
    }

    public bool IsLevelLost()
    {
        return levelLost;
    }

    public void LevelWin()
    {
        levelWon = true;
    }

    public void LevelLost()
    {
        levelLost = true;
    }

    public int LevelID()
    {
        return m_levelID;
    }

}
