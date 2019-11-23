using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Movement movement;
    private int playerID;
    private Camera m_cam;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerID = GetComponent<PlayerData>().PlayerID();
        m_cam = Camera.main;
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

        if (InputManager.JoystickRightHorizontal(playerID) != 0 || InputManager.JoystickRightVertical(playerID) != 0)
        {
            movement.Rotate();
        }

        if (InputManager.XButton(playerID))
        {

        }
        else if(Input.GetKey(KeyCode.Q))
        {

        }
    }
}
