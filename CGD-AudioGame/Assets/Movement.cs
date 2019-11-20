using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int player_ID;
    private Rigidbody2D rb2d;
    
    private void Start()
    {
        player_ID = GetComponent<PlayerData>().PlayerID();
        rb2d = GetComponent<Rigidbody2D>();
    }


    public void OnAnimatorMove()
    {

    }
    public void Rotate()
    {

    }
}
