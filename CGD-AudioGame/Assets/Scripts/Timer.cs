using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public LevelManager LevelManager;
    public Text timeText;

    private void Start()
    {
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        timeText = GetComponent<Text>();
        timeText.text = ((int)LevelManager.LevelTimer()).ToString();
    }

    private void Update()
    {
        timeText.text = ((int)LevelManager.LevelTimer()).ToString();
    }
}
