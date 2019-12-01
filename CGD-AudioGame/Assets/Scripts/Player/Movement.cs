using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0;
    private float footStepVolume;

    [Range(0, 10)]
    //Used for different floor types.
    [SerializeField] float[] FootStepVolumes;
    private int currentFloorType;

    private int playerID;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private float current_rotation;
    public Animator anim;
    private GameObject PlayerModel;
    public bool onStairs = false;
    private Vector3 stairStart;
    private Vector3 stairEnd;
    private float stairTimer = 3.8f;

    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        PlayerModel = GameObject.FindGameObjectWithTag("PlayerModel");
    }

    public void Update()
    {


    }

    private void FixedUpdate()
    {
        var currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            if (footStepVolume != 0)
            {
                SetFootstepVolume(0);
                anim.SetBool("Moving", false);
            }
        }
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

        if(onStairs)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, stairEnd, 0.05f);
            anim.SetBool("Moving", true);
            anim.SetFloat("InputMagnitude", 0.7f);
            stairTimer -= Time.fixedDeltaTime;
            if(stairTimer <= 0.0f)
            {
                stairTimer = 3.8f;
                onStairs = false;
                anim.SetBool("Moving", false);
                anim.SetFloat("InputMagnitude", 0.0f);
            }
        }

    }

    public void Move(bool keyboardInput)
    {
        if(keyboardInput)
        {
            if(!onStairs)
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(x, 0, z);
                float InputMagnitude = new Vector3(x, 0, z).magnitude;
                SetFootstepVolume(InputMagnitude);
                anim.SetFloat("InputMagnitude", InputMagnitude);
                anim.SetBool("Moving", true);
                //Debug.Log("Footstep Volume: " + footStepVolume);
                Rotate(true);
                transform.Translate((movement.normalized * (InputMagnitude * movementSpeed)) * Time.deltaTime);
            }
        }
        else
        {
            if(!onStairs)
            {
                //Controller
                float x = InputManager.JoystickHorizontal(playerID);
                float z = InputManager.JoystickVertical(playerID);
                Vector3 movement = new Vector3(x, 0, (z * -1));
                float InputMagnitude = new Vector3(x, 0, z).magnitude;
                SetFootstepVolume(InputMagnitude);
                anim.SetFloat("InputMagnitude", InputMagnitude);
                anim.SetBool("Moving", true);
                //Debug.Log("Footstep Volume: " + footStepVolume);
                Rotate(false);
                transform.Translate((movement.normalized * (InputMagnitude * movementSpeed)) * Time.deltaTime);
            }
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

    public void Rotate(bool kbd)
    {
        if(kbd)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            PlayerModel.transform.rotation = Quaternion.LookRotation(movement);

        }
        else
        {
            //Rotate the player on the Z axis
            //Calculate an angle here using the analogue sticks axis values.
            float go_direction = Mathf.Atan2(InputManager.JoystickVertical(playerID), InputManager.JoystickHorizontal(playerID));
            //Calculate radians to degrees.
            current_rotation = go_direction * Mathf.Rad2Deg + 90;
            PlayerModel.transform.eulerAngles = new Vector3(0, current_rotation, 0);
        }
    }

    void FloorType(string tagName)
    {
        //To do
        Debug.Log("On: "+tagName);
        switch(tagName)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float dist = Vector3.Distance(other.gameObject.transform.position, stairEnd);
        if (other.gameObject.CompareTag("Stair") && !onStairs)
        {
            onStairs = true;
            gameObject.transform.position = other.gameObject.GetComponent<endPosition>().startPos.position;
            PlayerModel.transform.rotation = other.gameObject.transform.rotation;
            stairEnd = other.gameObject.GetComponent<endPosition>().endPos.position;
        }

        //if (other.gameObject.CompareTag("Stair") && onStairs)
        //{
        //    onStairs = false;
        //    anim.SetBool("Moving", false);
        //    anim.SetFloat("InputMagnitude", 0.0f);
        //}
    }
}
