using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0;

    private int playerID;
    private Rigidbody2D rb2d;
    
    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move(bool keyboardInput)
    {
        if(keyboardInput)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            x *= movementSpeed * Time.deltaTime;
            y *= (movementSpeed * Time.deltaTime);

            transform.Translate(x, y, 0);
        }
        else
        {
            float x = InputManager.JoystickHorizontal(playerID);
            float y = InputManager.JoystickVertical(playerID);
            x *= movementSpeed * Time.deltaTime;
            y *= (movementSpeed * Time.deltaTime) *-1;

            transform.Translate(x, y, 0);
        }
    }

    //public void Rotate()
    //{
    //    float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
    //}
}
