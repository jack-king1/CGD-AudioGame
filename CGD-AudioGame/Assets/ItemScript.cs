using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class ItemScript : MonoBehaviour
{
    public FogOfWarScript fow;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("ManaCrystal");
        if (col.CompareTag("Player"))
        audio_controller = GameObject.Find("AudioController").GetComponent<PickupAudioController>();
        audio_controller.SetupSound(gameObject, PICKUP.mana);
        {

            fow.m_radius += 0.5f;
            fow.lamp.spotAngle += 10;
            //fow.lamp.color += (Color.white / 7.0f);

                audio_controller.PlaySound(gameObject);
             Destroy(gameObject);
        }
       
    }
}
