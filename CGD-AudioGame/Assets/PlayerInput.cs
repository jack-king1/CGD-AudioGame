using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Movement movement;
    private int playerID;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerID = GetComponent<PlayerData>().PlayerID();
    }

    void Update()
    {
        if(InputManager.AButton(playerID))
        {

        }

        if (InputManager.JoystickHorizontal(playerID) != 0 || InputManager.JoystickVertical(playerID) != 0)
        {
            movement.Move();
        }

        if (InputManager.XButton(playerID))
        {

        }
    }
}
