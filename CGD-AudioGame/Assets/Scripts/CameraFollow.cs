using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class CameraFollow : MonoBehaviour
{
    public GameObject m_target;
    [Range(0.01f, 1.0f)]
    public float m_cameraSpeed;
    public CAMERASTATE m_cameraState;
    private Vector3 cinematicEndLocation;
    private LevelManager m_levelManager;
    int playerID;
    float peekV;
    float peekH;
    [SerializeField]private float peekOffset = 3.0f;
    [SerializeField] private GameObject m_startCinematicCamPosition;
    [SerializeField] private float m_AttractCamSpinHeight;
    [SerializeField] private float m_AttractCamSpinSpeed;
    [SerializeField] private float cineTimerEnd;
    [SerializeField] private float timer;

    //Camera state variables
    //Player Follow
    [Range(0, 365)]
    public float cameraFollowPitch = 45.0f;

    //Player Won
    public float spinHeightabovePlayer = 3f;

    [Range(1f, 20f)]
    public float m_cameraZoomOffset = 5f;

    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
        cinematicEndLocation = m_target.transform.position;
        playerID = m_target.GetComponent<PlayerData>().PlayerID();
        m_startCinematicCamPosition = GameObject.FindGameObjectWithTag("CineStart");
        if(m_startCinematicCamPosition)
        {
            m_cameraState = CAMERASTATE.attract;
            gameObject.transform.position = m_startCinematicCamPosition.transform.position;
            transform.LookAt(m_target.transform.position);
        }
        else
        {
            m_cameraState = CAMERASTATE.follow;
            Debug.LogWarning("Cameras state set to FOLLOW. This scene/level currently has no cinematic camera. Speak to King if it needs one.");
        }
        m_levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if(m_levelManager.IsLevelWon() || m_levelManager.IsLevelLost())
        {
            if(m_cameraState != CAMERASTATE.levelwon)
            {
                m_cameraState = CAMERASTATE.levelwon;
            }
        }

        if(m_levelManager.GameState() == GAMESTATE.game && (m_levelManager.IsLevelWon() != true && m_levelManager.IsLevelLost()!= true))
        {
            m_cameraState = CAMERASTATE.follow;
        }

        switch (m_cameraState)
        {
            case CAMERASTATE.cinematic:
                CinematicCam();
                break;
            case CAMERASTATE.follow:
                FollowCam();
                break;
            case CAMERASTATE.levellost:
                LevelWon();
                break;
            case CAMERASTATE.levelwon:
                LevelWon();
                break;
            case CAMERASTATE.attract:
                Attract();
                break;
            default:
                break;
        }
    }

    //This function will be at the beginning or end or completed game and will have a start gameobject/transform and an end.
    void CinematicCam()
    {
        timer += Time.deltaTime;
        if(timer < 5)
        {
            Vector3 targetPosition = new Vector3(
            m_target.transform.position.x,
            m_target.transform.position.y + 3,
            m_target.transform.position.z - 2.5f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.005f);
        }
        else
        {
            m_cameraState = CAMERASTATE.follow;
            timer = 0;
        }
    }

    //Main gameloop camera.
    void FollowCam()
    {
        if (m_target)
        {
            //Position
            //transform.LookAt(m_target.transform.localPosition);
            Vector3 followPosition = new Vector3(
                m_target.transform.position.x + (peekH * peekOffset),
                m_target.transform.position.y + m_cameraZoomOffset,
                m_target.transform.position.z - 2.5f + (peekV *peekOffset));
            transform.position = Vector3.Slerp(transform.position, followPosition, m_cameraSpeed);

            Quaternion LookAtPlayer = new Quaternion(
            m_target.transform.rotation.x + cameraFollowPitch * Mathf.Deg2Rad,
            m_target.transform.rotation.y,
            m_target.transform.rotation.z,
            m_target.transform.rotation.w);

            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, LookAtPlayer, 0.01f);
        }
        else
        {
            Debug.LogError("You absolute mongrel! Camera can't find gameobject tagged with 'Player'. Please specify a target via" +
                " the script on the camera. https://www.youtube.com/watch?v=S8rRladhM1g ");
        }
    }

    void Attract()
    {
        transform.LookAt(m_target.transform);
        if (gameObject.transform.position.y != m_target.transform.position.y)
        {
            Vector3 targetPos = new Vector3(
                gameObject.transform.position.x,
                m_target.transform.position.y + m_AttractCamSpinHeight,
                gameObject.transform.position.z);

            gameObject.transform.position = Vector3.Slerp(transform.position, targetPos, 0.01f);
        }
        transform.Translate(Vector3.right * (Time.deltaTime * m_AttractCamSpinSpeed));
    }

    //Pan around player
    void LevelLost()
    {

    }

    //Pan around player.
    void LevelWon()
    {
        transform.LookAt(m_target.transform);
        if (gameObject.transform.position.y != m_target.transform.position.y)
        {
            Vector3 targetPos = new Vector3(
                gameObject.transform.position.x,
                m_target.transform.position.y + spinHeightabovePlayer,
                gameObject.transform.position.z);

            gameObject.transform.position = Vector3.Slerp(transform.position, targetPos, 0.01f);
        }
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void SetPeekValues(float h, float v)
    {
        peekH = h;
        peekV = v;
    }

    public void ResetLookAngle()
    {

    }
}