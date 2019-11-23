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
    private float current_rotation;

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
            float z = InputManager.JoystickVertical(playerID);
            Vector3 movement = new Vector3(x, 0, (z*-1));
            float InputMagnitude =  new Vector3(x, 0, z).magnitude;
            SetFootstepVolume(InputMagnitude);
            Debug.Log("Footstep Volume: " + footStepVolume);
            transform.Translate((movement.normalized * (InputMagnitude * movementSpeed) )* Time.deltaTime);
        }
    }

   //Audio 
   public void SetFootstepVolume(float InputMagnitude)
    {
        footStepVolume = InputMagnitude;
        //Debug.Log(footStepVolume);
    }

    public float FootStepVolume()
    {
        
        return footStepVolume;
    }

    public void Rotate()
    {
        //float z = InputManager.JoystickRightHorizontal(playerID);
        //float InputMagnitude = new Vector3(0, 0,z ).magnitude;
        //transform.Rotate((Vector3.up * z) * rotateSpeed  * Time.deltaTime);
    }
}
