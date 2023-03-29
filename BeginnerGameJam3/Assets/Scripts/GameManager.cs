using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Menu Components")]
    public bool isPaused = false;
    public GameObject currentMenu; //Tracks current menu to toggle
    public GameObject pauseMenu;

    //Load screen ?
    [Header("----- Scene Transition Components -----")]
    private AsyncOperation operation; //for loading a scene

    void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = pauseMenu;

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for pause menu button/s
            //Will have to add checks for what state the game is in
            //As well as controller input
        if(Input.GetButtonDown("Esc") )
        {
            if (currentMenu == pauseMenu)
            {
                isPaused = !isPaused;
                pauseMenu.SetActive(isPaused);
                if (isPaused)
                    gameLockPause();
                else
                    gameUnlockPause();
            }
        }
    }
    public void gameLockPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
    public void gameUnlockPause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}



