using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public FogOfWarScript fow;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (target.tag == "PlayerModel")
        {
            Debug.Log("Why");
            fow.m_radius += 0.5f;
            fow.lamp.spotAngle += 10;
            fow.lamp.color += (Color.white / 7.0f);

            Destroy(this.gameObject);
        }
    }
}
