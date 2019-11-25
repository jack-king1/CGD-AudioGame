using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"  || other.gameObject.tag == "Enemy")
        {
            ArrowTrap trap = transform.parent.gameObject.GetComponent<ArrowTrap>();
            trap.FireArrow();
        }       
    }
}
