using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{ 
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Coin Collided");
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerData>();
            Destroy(gameObject);
        }

    }

}
