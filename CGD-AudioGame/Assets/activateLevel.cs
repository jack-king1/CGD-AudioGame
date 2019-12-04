using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLevel : MonoBehaviour
{
    public GameObject levelToActivate;
    public GameObject levelToDeactivate;
    private Movement player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(player.onStairs)
            {
                levelToActivate.SetActive(true);
                levelToDeactivate.SetActive(false);
            }
        }
    }
}
