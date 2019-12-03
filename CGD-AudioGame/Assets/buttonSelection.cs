using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
using System;

public class buttonSelection : MonoBehaviour
{
    public int index;
    public bool returnToGame;

    //[SerializeField] bool keyDown;
    [SerializeField] int maxIndex;

    //PlayerInput inputScript;

    private LevelManager m_levelManager;
    private setFullscreen fullscreen;

    private int playerID;

    void Start()
    {
        //inputScript = gameObject.GetComponent<PlayerInput>();
        m_levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        fullscreen = GameObject.Find("MenuUI").GetComponent<setFullscreen>();
        playerID = GetComponent<PlayerData>().PlayerID();

        returnToGame = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = maxIndex;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (index < maxIndex)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        
        if (m_levelManager.GameState() == GAMESTATE.pause)
        {
            if (Input.GetKeyDown(KeyCode.Return) || InputManager.AButton(playerID))
            {

                if (Input.GetAxisRaw("J_Horizontal_1") == 1)
                {
                    playerID++;
                }

                
                switch (index)
                {
                    case 0:
                        m_levelManager.GameState(GAMESTATE.game);
                        break;
                    case 1:
                        m_levelManager.GameState(GAMESTATE.audioSettings);
                        break;
                    case 2:
                        Application.Quit();
                        break;
                    case 3:
                        if (fullscreen.fullScreenActivated == false)
                        {
                            fullscreenActivate();
                        }
                        else
                        {
                            fullscreenDeactivate();
                        }
                        break;
                }
            }
        }
        else if (m_levelManager.GameState() == GAMESTATE.audioSettings)
        {
            if (Input.GetKeyDown(KeyCode.Return) || InputManager.AButton(playerID))
            {
                switch (index)
                {
                    case 1:
                        m_levelManager.GameState(GAMESTATE.pause);
                        break;
                }
            }
        }
    }

    private void fullscreenActivate()
    {
        fullscreen.fullScreenActivated = true;
        Debug.Log("YESS");
    }

    private void fullscreenDeactivate()
    {
        fullscreen.fullScreenActivated = false;
        Debug.Log("OFFNOW");
    }
}
