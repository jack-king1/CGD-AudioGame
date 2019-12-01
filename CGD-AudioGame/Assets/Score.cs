using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text t_score;
   public float PlayerScore = 0.0f;
    public string test = "Nonce";

    void Start()
    {
        t_score.gameObject.SetActive(false);
      
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateScore()
    {
        t_score.gameObject.SetActive(true);
        int scoreToInt = (int)PlayerScore;
        t_score.text = ("Score: "+scoreToInt.ToString());
     
    }

}
