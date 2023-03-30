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

    [Header("Player Objects")]
    [SerializeField] public GameObject Player1;
    [SerializeField] public GameObject Player2;

    [Header("Game Loop Components")]
    public TextMeshProUGUI timeRemainingText;
    [SerializeField] float roundTimer;
    private float timeRemaining;
    [SerializeField] int numberOfRounds;
    static public int currentRound;
    private bool roundOver;
    //[SerializeField] 

    [Header("Menu Components")]
    public bool isPaused = false;
    public GameObject pauseUI;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] AudioSource _pauseUIMusic;
    [SerializeField] AudioSource _loadScreenMusic;

    [Header("----- Scene Transition Components -----")]
    private AsyncOperation operation; //for loading a scene
    public int sceneIndex;

    [Header("----- Round Transition Text -----")]
    public GameObject Fight;
    public GameObject KO;
    public GameObject TimeUp;
    public GameObject Tie;

    [Header("----- Load Screen Components -----")]
    public bool isLoading;
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
        timeRemaining = roundTimer;
    }

    void Start()
    {
        Time.timeScale = 1;
        isLoading = false;
        roundOver = false;
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
            LoadProgress();
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
        else if (!isPaused && !isLoading)
        {
            GameLogic();
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

    public void GameLogic()
    {
        //add tempRemaining check for on screen text 
        if (roundOver)
        {
            if (timeRemaining <= 0)
            {
                if (Player1.GetComponent<PlayerController>().Winner || Player2.GetComponent<PlayerController>().Winner)
                {
                    QuitGame(); //Temp instead of game over screen
                }
                else if (currentRound < 3)
                {
                    PauseGame();
                    RestartGame();
                }
                else
                {
                    QuitGame(); //Temp instead of game over screen
                }
            }
            else
            {
                timeRemaining -= Time.deltaTime;
            }
        }
        else
        {
            if (timeRemaining > roundTimer - 1)
            {
                Time.timeScale = 0.5f;
                Fight.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Fight.SetActive(false);
            }
            if (Player1.GetComponent<CombatController>().isDead || Player2.GetComponent<CombatController>().isDead)
                RoundOver(1);
            else
            {
                timeRemaining -= Time.deltaTime;
                timeRemainingText.text = System.Math.Round(timeRemaining, 0).ToString();
                if (timeRemaining <= 0)
                {
                    RoundOver(2);
                }
            }
        }
    }

    public void RoundOver(int condition)
    {
        switch (condition)
        {
            case 1: //A Player has died
                if (Player2.GetComponent<CombatController>().isDead)
                {
                    Player1.GetComponent<PlayerController>().incrementRoundsWon();
                }
                else if (Player1.GetComponent<CombatController>().isDead)
                {
                    Player2.GetComponent<PlayerController>().incrementRoundsWon();
                }
                timeRemaining = 2;
                Time.timeScale = 0.5f;
                KO.SetActive(true);
                break;
            case 2: //Round timer is up
                if(Player1.GetComponent<CombatController>()._totalHealth == Player2.GetComponent<CombatController>()._totalHealth)
                {
                    timeRemaining = 2;
                    Time.timeScale = 0.5f;
                    Tie.SetActive(true);
                }
                else if (Player1.GetComponent<CombatController>()._totalHealth > Player2.GetComponent<CombatController>()._totalHealth)
                {
                    Player1.GetComponent<PlayerController>().incrementRoundsWon();
                    timeRemaining = 2;
                    Time.timeScale = 0.5f;
                    TimeUp.SetActive(true);
                }
                else
                {
                    Player2.GetComponent<PlayerController>().incrementRoundsWon();
                    timeRemaining = 2;
                    Time.timeScale = 0.5f;
                    TimeUp.SetActive(true);
                }
                break;
            default:
                timeRemaining = 2;
                Time.timeScale = 0.5f;
                Tie.SetActive(true);
                //Only thing that would fall here is a tie
                break;
        }
        currentRound++;
        roundOver = true;
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
        pauseUI.SetActive(false);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene();
    }
    public void LoadProgress()
    {
        targetValue = operation.progress / 0.9f;

        currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.unscaledDeltaTime);
        progressBar.fillAmount = currentValue;

        progressText.text = System.Math.Round(currentValue * 100f, 0) + "%";

        if (Mathf.Approximately(currentValue, 1))
        {
            operation.allowSceneActivation = true;
            //isLoading = false;
            _loadScreenMusic.Stop();
            ResumeGame();
        }
    }
    public void LoadScene()
    {
        loadScreen.SetActive(true);
        _loadScreenMusic.Play();
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        isLoading = true;
    }
}



