using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpoint;
    GameObject player;
    bool found;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void OnTriggerEnter(Collider plyr)
    {
        if (!found)
        {
            if (plyr.gameObject.tag == "Player")
            {
                found = true;
                player.gameObject.GetComponent<PlayerCP>().activeCP=checkpoint;
            }
        }
    }
}
