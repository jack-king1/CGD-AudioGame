using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAudioDampening : MonoBehaviour
{
    public List<GameObject> enemies;
    public LayerMask excluded_layers;
    EnemyAudioController audio_controller;

    private void Start()
    {
        audio_controller = GameObject.Find("AudioController").GetComponent<EnemyAudioController>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }
    private void Update()
    {      
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, enemies[i].transform.position - transform.position, out hit, 100))
                {
                    if (hit.transform.gameObject.tag == "Wall")
                    {
                        Debug.Log("Dampening " + i);
                        audio_controller.SetVolMultiplier(enemies[i], true);
                        return;
                    }
                    else
                    {
                        audio_controller.SetVolMultiplier(enemies[i], false);
                        Debug.Log("Not Dampening " + i);
                    }
                }
            }
        }
    }
}
