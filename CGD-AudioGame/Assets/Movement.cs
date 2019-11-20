using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private int playerID;
    private Rigidbody2D rb2d;
    
    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {

    }

    //public void Rotate()
    //{
    //    float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
    //}
}
