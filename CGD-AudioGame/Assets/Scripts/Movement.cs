using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0;
    private float footStepVolume;

    [Range(0,10)]
    [SerializeField] float[] FootStepVolumes;

    private int playerID;
    private Rigidbody rb;
    private Vector3 lastPosition;
    
    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        var currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            SetFootstepVolume(0);
        }


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

   //Audio 
   public void SetFootstepVolume(int soundID)
    {
        footStepVolume = FootStepVolumes[soundID];
    }

    public float FootStepVolume()
    {
        return footStepVolume;
    }

    //public void Rotate()
    //{
    //    float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
    //}
}
