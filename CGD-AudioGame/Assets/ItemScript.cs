using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class ItemScript : MonoBehaviour
{
    public FogOfWarScript fow;
    PickupAudioController audio_controller;
    public SkinnedMeshRenderer Skinned;
    public PlayerData Pd;
    // Start is called before the first frame update
    void Start()
    {
        Skinned.enabled = false;
        audio_controller = GameObject.Find("AudioController").GetComponent<PickupAudioController>();
        audio_controller.SetupSound(gameObject, PICKUP.mana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {

        if (this.tag != "Enemy")
        {


            if (other.tag == "PlayerModel")
            {
                audio_controller.PlaySound(gameObject);
                fow.m_radius += 0.5f;
                fow.lamp.spotAngle += 10;
                fow.lamp.color += (Color.white / 7.0f);

                Destroy(this.gameObject);
            }
        }



        if(other.tag == "CullRange")
        { 
            Skinned.enabled = true;

        }

       
    }


    private void OnTriggerExit(Collider other)
    {
      
        Skinned.enabled = false;
    }
}
