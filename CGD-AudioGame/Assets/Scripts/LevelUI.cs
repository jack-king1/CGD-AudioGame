using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public LevelManager LevelManager;
    public Text levelText;

    private void Start()
    {
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        levelText = GetComponent<Text>();
        levelText.text += LevelManager.LevelID().ToString();
    }

}
