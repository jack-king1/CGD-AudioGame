using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActive : MonoBehaviour
{
    public List<GameObject> levels;
    void Start()
    {
        foreach(var level in levels)
        {
            level.SetActive(false);
        }

        levels[0].SetActive(true);
    }


}
