
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
// for the countdown
    bool countdownRunning = false;
    public float timeRemaining = 3;
    public TMP_Text CountdownText;
    public Button startButton;

    public GameObject CountdownVisual;
    public GameObject goVisual;

// making the ball fall
    public GameObject ballStopper;

// for the timer
    public GameObject timerVisual;
    public TMP_Text timerText;
    public TMP_Text lastScoreText;
    float timeOfTimer = 0;
    public bool timerRunning = false;

    // getting bool from stop timer from ball
    public GameObject stopTimerObject;
    private StopTimer stopTimer;

    // level end
    bool levelEnded = false;
    bool restartClicked = false;
    public Button restartButton;
    public GameObject showResultVisual;
    public TMP_Text showResultText;
    public TMP_Text lastScoreToCompare;


    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startCountdown);

        restartButton.onClick.AddListener(restartScene);

        stopTimer = stopTimerObject.GetComponent<StopTimer>();

        var (minutes, seconds, milliSeconds) = convertTimeOfTimer(PlayerPrefs.GetFloat("lastRound"));
        
        lastScoreText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        // Countdown when starting the game
        if(countdownRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining > 0)
            {
                displayCountdown(timeRemaining);
            }
            else if(timeRemaining <= 0 && timeRemaining > -1 )
            {

                CountdownVisual.gameObject.SetActive(false);
                goVisual.gameObject.SetActive(true);
                ballStopper.gameObject.SetActive(false);
                
                timerRunning = true;
            }
            else if (timeRemaining < -1 && timeRemaining > -2)
            {
                goVisual.gameObject.SetActive(false);
                countdownRunning = false;       
                timeRemaining = -2;
            }
        }
        else
        {
            CountdownVisual.gameObject.SetActive(false);
        }
        
        if (timerRunning)
        {
            timerVisual.gameObject.SetActive(true);

            timeOfTimer += Time.deltaTime;
            showTimer(timeOfTimer);
        }

        if (stopTimer.ballStop)
        {
            // showTimer(timeOfTimer);
            timerRunning = false;
            
            if (!levelEnded)
            {
                levelEnded = true;

                levelEnding(timeOfTimer);
                showResultVisual.gameObject.SetActive(true);
                CountdownVisual.gameObject.SetActive(false);
                showTimer(timeOfTimer);
            }

        }
    }

    void startCountdown()
    {
        countdownRunning = true;
        CountdownVisual.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
    }
    void displayCountdown(float timeRemaining)
    {
        string seconds = timeRemaining.ToString("F0");

        CountdownText.text = string.Format(seconds);
    }

    static (float Minutes, float Seconds, float MilliSeconds) convertTimeOfTimer(float timeInSeconds)
    {
        float minutes = Mathf.FloorToInt(timeInSeconds / 60); 
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);
        float milliSeconds = (timeInSeconds % 1) * 1000;

        return (minutes, seconds, milliSeconds);
    }

    void showTimer(float timeOfTimer)
    {
        var (minutes, seconds, milliSeconds) = convertTimeOfTimer(timeOfTimer);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);

        if (levelEnded){
        showResultText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        }
    }

    void levelEnding(float timeOfTimer)
    {

        timerVisual.gameObject.SetActive(false);

        var (minutes, seconds, milliSeconds) = convertTimeOfTimer(PlayerPrefs.GetFloat("lastRound"));
        
        lastScoreToCompare.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);

        saveLastTime(timeOfTimer);

    }

    void saveLastTime(float timeOfTimer)
    {
        PlayerPrefs.SetFloat("lastRound", timeOfTimer);
        Debug.Log("save last time executed. Last round = " + timeOfTimer);
    }

    void restartScene()
    {
      SceneManager.LoadScene("Maze_Start");  
    }
}
