using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class ArrowButton : MonoBehaviour
{
    TrapAudioController audio_controller;
    private void Start()
    {
        audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        audio_controller.SetupSound(gameObject, TRAP.arrow_btn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"  || other.gameObject.tag == "Enemy")
        {
            audio_controller.PlaySound(TRAP.arrow_btn, gameObject);
            ArrowTrap trap = transform.parent.gameObject.GetComponent<ArrowTrap>();
            trap.FireArrow();
        }       
    }
}
