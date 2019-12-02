using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScoreText : MonoBehaviour
{
    public Text scoreText;
    private LevelManager lm;

    private void Awake()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        if(!lm.IsLevelLost() && !lm.IsLevelWon())
        {
            int localScoreToInt = (int)lm.score.playerScore;
            scoreText.text = ("Score: ") + localScoreToInt.ToString();
        }
        else
        {
            if (lm.IsLevelLost())
            {
                int localScoreToInt = (int)lm.score.CalculateScore(false, lm.LevelTimer());
                scoreText.text = ("Score: ") + localScoreToInt.ToString();
            }
            else
            {
                int localScoreToInt = (int)lm.score.CalculateScore(true, lm.LevelTimer());
                scoreText.text = ("Score: ") + localScoreToInt.ToString();
            }
        }
    }
}
