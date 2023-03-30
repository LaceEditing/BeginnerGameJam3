using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Menu Components")]
    public bool isPaused = false;
    public GameObject pauseUI;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] AudioSource _pauseUIMusic;
    [SerializeField] AudioSource _loadScreenMusic;


    [Header("----- Scene Transition Components -----")]
    private AsyncOperation operation; //for loading a scene

    [Header("----- Load Screen Components -----")]
    public bool isLoading;
    public int sceneIndex;
    public GameObject loadScreen;
    public Image progressBar;
    public TextMeshProUGUI progressText;
    private float currentValue;
    private float targetValue;
    [SerializeField] [Range(0, 1)] private float progressAnimationMultiplier = 0.25f;

    void Awake()
    {
        instance = this;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        progressBar.fillAmount = currentValue = targetValue = 0;
    }

    void Start()
    {
        Time.timeScale = 1;
        _backgroundMusic = GameObject.Find("backgroundMusic").GetComponent<AudioSource>();
        _pauseUIMusic = GameObject.Find("pauseMusic").GetComponent<AudioSource>();
        _pauseUIMusic.Stop();
        _loadScreenMusic = GameObject.Find("LoadScreen").GetComponent<AudioSource>();
        _loadScreenMusic.Stop();
    }
    void Update()
    {
        if (isLoading)
        {
            loadProgress();
        }
        if (Input.GetButtonDown("Esc") && !isLoading)
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

        _backgroundMusic.Pause();
        _pauseUIMusic.Play();
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
        _pauseUIMusic.Stop();
        _backgroundMusic.UnPause();
        pauseUI.SetActive(false);
    }

    public void QuitGame()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit(); 
    }
    public void RestartGame()
    {
        _pauseUIMusic.Stop();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        loadScene();
    }
    public void loadProgress()
    {
        targetValue = operation.progress / 0.9f;

        currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.unscaledDeltaTime);
        progressBar.fillAmount = currentValue;

        progressText.text = System.Math.Round(currentValue * 100f, 0) + "%";

        if (Mathf.Approximately(currentValue, 1))
        {
            operation.allowSceneActivation = true;
            isLoading = false;
            _loadScreenMusic.Stop();
            ResumeGame();
        }
    }
    public void loadScene()
    {
        loadScreen.SetActive(true);
        _loadScreenMusic.Play();
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        isLoading = true;
    }
}



