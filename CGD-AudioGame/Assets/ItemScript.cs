using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public FogOfWarScript fow;
    public SkinnedMeshRenderer Skinned;
    public PlayerData Pd;

    // Start is called before the first frame update
    void Start()
    {
        Skinned.enabled = false;
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

                fow.m_radius += 0.5f;
                fow.lamp.spotAngle += 10;
           

                Destroy(this.gameObject);
            }
        }



        if(other.tag == "CullRange")
        {
            Debug.Log("Why");
           
            Skinned.enabled = true;

        }

       
    }


    private void OnTriggerExit(Collider other)
    {
      
        Skinned.enabled = false;
    }
}
