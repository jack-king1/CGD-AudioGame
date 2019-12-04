using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public GameObject[] Chests;
    private LevelManager lm;

    private void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }


    private void Update()
    {
        int animPlayed = 0;
        foreach(var chest in Chests)
        {
            if(chest.GetComponent<ChestOpen>().animPlayed)
            {
                animPlayed++;
            }
        }

        if(animPlayed == 2)
        {
            lm.LevelWin();
        }
    }
}
