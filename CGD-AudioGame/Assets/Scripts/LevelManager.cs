using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float m_levelTimer = 60;

    private float timerResetValue;
    [SerializeField] private int m_levelID = 0;
    public bool m_startLevel = false;
    private bool levelWon = false;
    private bool levelLost = false;
    GAMESTATE m_gameState = GAMESTATE.attract;
    public Transform m_PlayerStartPos;
    public Transform m_PlayerFinishPos;
    private GameObject m_Player;
    public GameObject playerPrefab;
    public PlayerCP pchkpt;


    public Score score;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        score = GetComponent<Score>();
        timerResetValue = m_levelTimer;
        pchkpt = GetComponent<PlayerCP>();
        pchkpt.activeCP = m_PlayerStartPos;
    }

    private void Update()
    {
        if (m_startLevel && m_gameState == GAMESTATE.game)
        {
            if(m_levelTimer > 0)
            {
                m_levelTimer -= Time.deltaTime;
                if (!m_Player)
                {
                   m_Player = Instantiate(playerPrefab, pchkpt.activeCP.transform.position, Quaternion.identity);
                }
            }
            else if(m_levelTimer <= 0)
            {
                LevelLost();
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

    public GAMESTATE GameState()
    {
        return m_gameState;
    }

    public void GameState(GAMESTATE gs)
    {
        m_gameState = gs;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseScene()
    {
        SceneManager.LoadScene(0);
    }
}
