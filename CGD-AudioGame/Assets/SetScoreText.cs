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
        int localScoreToInt = (int)lm.score.playerScore;
        scoreText.text = localScoreToInt.ToString();
    }
}
