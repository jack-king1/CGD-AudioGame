using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public FogOfWarScript fow;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("ManaCrystal");
        if (col.CompareTag("Player"))
        {

            fow.m_radius += 0.5f;
            fow.lamp.spotAngle += 10;
            //fow.lamp.color += (Color.white / 7.0f);

             Destroy(gameObject);
        }
       
    }
}
