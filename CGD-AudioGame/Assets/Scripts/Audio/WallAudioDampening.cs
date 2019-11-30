using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAudioDampening : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> traps;
    public LayerMask excluded_layers;
    EnemyAudioController en_audio_controller;
    TrapAudioController tr_audio_controller;

    private void Start()
    {
        en_audio_controller = GameObject.Find("AudioController").GetComponent<EnemyAudioController>();
        tr_audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        traps = new List<GameObject>(GameObject.FindGameObjectsWithTag("Trap"));
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
                        en_audio_controller.SetVolMultiplier(enemies[i], true);
                        return;
                    }
                    else
                    {
                        en_audio_controller.SetVolMultiplier(enemies[i], false);
                    }
                }
            }
        }
        for (int i = 0; i < traps.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, traps[i].transform.position - transform.position, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Wall")
                {
                    tr_audio_controller.SetVolMultiplier(traps[i], true);
                    return;
                }
                else
                {
                    tr_audio_controller.SetVolMultiplier(traps[i], false);
                }
            }
        }
    }
}
