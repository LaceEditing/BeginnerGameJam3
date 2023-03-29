using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Menu Components")]
    public bool isPaused = false;
    public GameObject pauseUI;

    [Header("----- Scene Transition Components -----")]
    private AsyncOperation operation; //for loading a scene

    void Awake()
    {
        instance = this;

    }

    void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        if(Input.GetButtonDown("Esc") )
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isPaused = false;
        pauseUI.SetActive(false);
    }

    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit(); 
    }
}



