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
        else if(Input.GetKey(KeyCode.E))
        {
            //Attck or something?
        }

        if (InputManager.JoystickHorizontal(playerID) != 0 || InputManager.JoystickVertical(playerID) != 0)
        {
            movement.Move(false);
        }
        else if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            movement.Move(true);
        }

        if (InputManager.XButton(playerID))
        {

        }
        else if(Input.GetKey(KeyCode.Q))
        {

        }
    }
}
