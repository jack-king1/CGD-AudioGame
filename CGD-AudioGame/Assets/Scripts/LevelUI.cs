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

        //+ 1 because otherwise it messes with scene manager, loading the right scene based on level id.
        int levelID = LevelManager.LevelID() + 1;
        levelText.text += levelID.ToString();
    }

}
