using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class PlayerInput : MonoBehaviour
{
    private Movement movement;
    private int playerID;
    private Camera m_cam;
    private CameraFollow m_camerafollow;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerID = GetComponent<PlayerData>().PlayerID();
        m_cam = Camera.main;
        m_camerafollow = m_cam.GetComponent<CameraFollow>();
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

        if (InputManager.JoystickRightHorizontalRaw(playerID) != 0 || InputManager.JoystickRightVerticalRaw(playerID) != 0)
        {
            float h = InputManager.JoystickRightHorizontalRaw(playerID);
            float v = InputManager.JoystickRightVerticalRaw(playerID);

            m_camerafollow.SetPeekValues(h, v*-1);
        }
        else
        {
            m_camerafollow.SetPeekValues(0, 0);
        }

        if (InputManager.XButton(playerID))
        {

        }
        else if(Input.GetKey(KeyCode.Q))
        {

        }
    }
}
