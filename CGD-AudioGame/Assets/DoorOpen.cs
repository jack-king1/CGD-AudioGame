using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorOpen : MonoBehaviour
{
    private GameObject Door;

    private void OnTriggerEnter(Collider col)
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(col.CompareTag("Player"))
                {
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(Door.gameObject);
                }
            }
        }
    }
}
