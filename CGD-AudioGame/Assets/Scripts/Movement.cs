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
            if(footStepVolume != 0)
            {
                SetFootstepVolume(0);
                Debug.Log("Step Volume Set to 0");
            }
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
            Debug.Log("X axis: " + x);
            float InputMagnitude =  new Vector3(x, 0, y).magnitude;
            SetFootstepVolume(InputMagnitude);
            Vector3 normalizedmovement = new Vector3(x, 0, y*-1).normalized;
            transform.Translate((normalizedmovement * (InputMagnitude * movementSpeed) )* Time.deltaTime);
        }
    }

   //Audio 
   public void SetFootstepVolume(float InputMagnitude)
    {
        
        footStepVolume = InputMagnitude;
        Debug.Log(footStepVolume);
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
