using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private Movement movement;
    private int playerID;
    private Camera m_cam;
    private CameraFollow m_camerafollow;
    private LevelManager m_levelManager;

    public bool audioButtonClicked;
    public bool backButtonClicked;
    public bool quitButtonClicked;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerID = GetComponent<PlayerData>().PlayerID();
        m_cam = Camera.main;
        m_camerafollow = m_cam.GetComponent<CameraFollow>();
        m_levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        if(!m_levelManager)
        {
            Debug.LogError("Mongrel! Add a level manager and hopefully movement should work.");
        }

        audioButtonClicked = false;
        backButtonClicked = false;
        quitButtonClicked = false;
}

    void Update()
    {
        //Attract State
        if(m_levelManager.GameState() == GAMESTATE.attract)
        {
            if (InputManager.AButton(playerID))
            {
                m_levelManager.GameState(GAMESTATE.game);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                m_levelManager.GameState(GAMESTATE.game);
            }
        }

        //Game State
        if (m_levelManager.GameState() == GAMESTATE.game )
        {
            if(!m_levelManager.IsLevelLost() && !m_levelManager.IsLevelWon())
            {
                if (InputManager.JoystickHorizontal(playerID) != 0 || InputManager.JoystickVertical(playerID) != 0)
                {
                    movement.Move(false);
                }
                else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    movement.Move(true);
                }

                // Pausing game - settings menu will appear
                if (InputManager.StartButtonDown(playerID))
                {
                    Pause();
                }
                else if(Input.GetKeyDown(KeyCode.P))
                {
                    Pause();
                }

                if (InputManager.JoystickRightHorizontalRaw(playerID) != 0 || InputManager.JoystickRightVerticalRaw(playerID) != 0)
                {
                    float h = InputManager.JoystickRightHorizontalRaw(playerID);
                    float v = InputManager.JoystickRightVerticalRaw(playerID);

                    m_camerafollow.SetPeekValues(h, v * -1);
                }
                else if (Input.GetAxisRaw("Horizontal_Arrow") != 0 || Input.GetAxisRaw("Vertical_Arrow") != 0)
                {
                    float h = Input.GetAxisRaw("Horizontal_Arrow");
                    float v = Input.GetAxisRaw("Vertical_Arrow");

                    m_camerafollow.SetPeekValues(h, v);
                }
                else
                {
                    m_camerafollow.SetPeekValues(0, 0);
                }
                if (InputManager.XButton(playerID))
                {

                }
                else if (Input.GetKey(KeyCode.Q))
                {

                }
            }
            else if(m_levelManager.IsLevelWon())
            {
                if (InputManager.AButton(playerID))
                {
                    m_levelManager.NextLevel();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    m_levelManager.NextLevel();
                }
            }
            else if(m_levelManager.IsLevelLost())
            {
                if (InputManager.AButton(playerID))
                {
                    m_levelManager.ResetLevel();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    m_levelManager.ResetLevel();
                }
            }
        }
        else if(m_levelManager.GameState() == GAMESTATE.pause)
        {
            if (InputManager.StartButtonDown(playerID))
            {
                Continue();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Continue();
            }

            if (audioButtonClicked == true)
            {
                m_levelManager.GameState(GAMESTATE.audioSettings);
            }

            //if (quitButtonClicked == true)
            //{
            //    Application.Quit();
            //}
        }
        else if (m_levelManager.GameState() == GAMESTATE.audioSettings)
        {
            if (backButtonClicked == true)
            {
                m_levelManager.GameState(GAMESTATE.pause);
            }
        }
    }
    public void Quit()
    {
        quitButtonClicked = true;
        Application.Quit();
    }

    public void AudioButtonClicked()
    {
        backButtonClicked = false;
        audioButtonClicked = true;
    }

    public void BackButtonClicked()
    {
        audioButtonClicked = false;
        backButtonClicked = true;
    }

    public void Continue()
    {
        //Time.timeScale = 1;
        m_levelManager.GameState(GAMESTATE.game);
    }

    public void Pause()
    {
        //Time.timeScale = 0;
        m_levelManager.GameState(GAMESTATE.pause);
    }
}
