using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0;

    private int playerID;
    private Rigidbody rb;
    
    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb = GetComponent<Rigidbody>();
    }

    public void Move(bool keyboardInput)
    {
        if(keyboardInput)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            x *= movementSpeed * Time.deltaTime;
            y *= (movementSpeed * Time.deltaTime);

            transform.Translate(x, 0, y);
        }
        else
        {
            float x = InputManager.JoystickHorizontal(playerID);
            float y = InputManager.JoystickVertical(playerID);
            x *= movementSpeed * Time.deltaTime;
            y *= (movementSpeed * Time.deltaTime) *-1;
            Vector3 normalizedmovement = new Vector3(x, 0, y).normalized;

            transform.Translate(normalizedmovement * movementSpeed * Time.deltaTime);
        }
    }

    //public void Rotate()
    //{
    //    float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
    //}
}
