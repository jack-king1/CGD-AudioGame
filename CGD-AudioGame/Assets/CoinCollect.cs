using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{ 
    public float scoreAmount = 100;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Coin Collided");
        if (col.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Score>().playerScore += scoreAmount;
            Destroy(gameObject);
        }

    }

}
