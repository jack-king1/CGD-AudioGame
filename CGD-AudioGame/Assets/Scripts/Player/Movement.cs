using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
[RequireComponent(typeof(PlayerData), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0;
    private float footStepVolume;

    [Range(0,10)]
    [SerializeField] float[] FootStepVolumes;
    private int currentFloorType;

    private int playerID;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private float current_rotation;
    public Animator anim;
    private GameObject PlayerModel;
    FootstepAudioController audio_controller;
    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        PlayerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        audio_controller = GameObject.Find("AudioController").GetComponent<FootstepAudioController>();
        audio_controller.SetupSound(gameObject, FOOTSTEP.player);
    }

    public void Update()
    {
        if (anim.GetBool("Moving"))
        {
            if (!audio_controller.IsPlaying(gameObject))
            {
                audio_controller.PlaySound(gameObject);
            }
        }
        else
        {
            if (audio_controller.IsPlaying(gameObject))
            {
                audio_controller.StopSound(gameObject);
            }
        }

        var currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            if(footStepVolume != 0)
            {
                SetFootstepVolume(0);
                anim.SetBool("Moving", false);              
            }
        }
    }

    private void FixedUpdate()
    {
        float dist = 3;
        Vector3 dir = new Vector3(0, -1, 0);
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir * dist, Color.green);

        if (Physics.Raycast(transform.position, dir, out hit, dist, LayerMask.GetMask("Walkable")))
        {
            //the ray collided with something, you can interact
            // with the hit object now by using hit.collider.gameObject
            FloorType(hit.collider.tag);
        }
        else
        {

        }
    }

    public void Move(bool keyboardInput)
    {
        if(keyboardInput)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0, z );
            float InputMagnitude = new Vector3(x, 0, z).magnitude;
            SetFootstepVolume(InputMagnitude);
            anim.SetFloat("InputMagnitude", InputMagnitude);
            anim.SetBool("Moving", true);
            //Debug.Log("Footstep Volume: " + footStepVolume);
            Rotate();
            transform.Translate((movement.normalized * (InputMagnitude * movementSpeed)) * Time.deltaTime);
        }
        else
        {
            //Controller
            float x = InputManager.JoystickHorizontal(playerID);
            float z = InputManager.JoystickVertical(playerID);
            Vector3 movement = new Vector3(x, 0, (z*-1));
            float InputMagnitude =  new Vector3(x, 0, z).magnitude;
            SetFootstepVolume(InputMagnitude);
            anim.SetFloat("InputMagnitude", InputMagnitude);
            anim.SetBool("Moving", true);
            //Debug.Log("Footstep Volume: " + footStepVolume);
            Rotate();
            transform.Translate((movement.normalized * (InputMagnitude * movementSpeed) )* Time.deltaTime);
        }
    }

   //Audio 
   public void SetFootstepVolume(float InputMagnitude)
    {
        footStepVolume = InputMagnitude + FootStepVolumes[currentFloorType];
        //Debug.Log(footStepVolume);
    }

    public float FootStepVolume()
    {
        return footStepVolume;
    }

    public void Rotate()
    {
        //Rotate the player on the Z axis
        //Calculate an angle here using the analogue sticks axis values.
        float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
        //Calculate radians to degrees.
        current_rotation = go_direction * Mathf.Rad2Deg + 90;
        PlayerModel.transform.eulerAngles = new Vector3(0, current_rotation, 0);
    }

    void FloorType(string tagName)
    {
        //To do
        Debug.Log("On: "+tagName);
        switch(tagName)
        {

        }
    }
}
