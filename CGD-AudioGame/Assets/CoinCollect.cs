using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private PlayerData Pd;
   
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Coin Collided");
        if (col.CompareTag("Player"))
        {
            //Add player score here bois.
            Pd.playerScore += 1.0f;

            Destroy(gameObject);
        }

    }

}
