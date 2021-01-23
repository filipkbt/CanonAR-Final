using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonGameBehaviour : EventDrivenObject
{

    public int defaultTimeInSeconds = 60;

    private int score;
    private float time;
    private int shots;
    private int hits;

    private float lastTime;
    private float startTime;

    private GameObject cameraCannon;
    private GameObject launchSource;

    private GameObject gameOverlay;
    private Text scoreText;

    private Text timerText;

    private GameObject gameOverPanel;

    private Text gameOverScore;

    private Text gameOverTime;

    private Text gameOverAccuracy;

    private bool gameInProgress;

    private bool gameOver;

    void Awake()
    {
        gameInProgress = false;
        gameOver = false;
        hits = 0;
        shots = 0;
        score = 0;
        time = 0;


        cameraCannon = GameObject.Find("CameraCannon");
        launchSource = GameObject.Find("LaunchSource");

        cameraCannon.SetActive(false);
        launchSource.SetActive(false);
        gameOverlay = transform.Find("CannonGameOverlay").gameObject;
        scoreText = gameOverlay.transform.Find("ScoreContainer/ScoreValue").gameObject.GetComponent<Text>();
        timerText = gameOverlay.transform.Find("TimerContainer/TimerValue").gameObject.GetComponent<Text>();

        gameOverPanel = gameOverlay.transform.Find("GameOverPanel").gameObject;
        gameOverScore = gameOverPanel.transform.Find("GameOverStatsPanel/ScoreValue").gameObject.GetComponent<Text>();
        gameOverTime = gameOverPanel.transform.Find("GameOverStatsPanel/TotalTimeValue").gameObject.GetComponent<Text>();
        gameOverAccuracy = gameOverPanel.transform.Find("GameOverStatsPanel/AccuracyValue").gameObject.GetComponent<Text>();

        RegisterEvent("onCannonTapped", OnCannonTapped);
        RegisterEvent("onTargetHit", OnTargetHit);
        RegisterEvent("onCannonFired", OnShotTaken);
        RegisterEvent("onImageTargetLost", OnImageTargetLost);
    }

    void Update()
    {
        if (gameInProgress && !gameOver)
        {
            float dif = Time.time - lastTime;
            if (dif >= 1)
            {
                lastTime = Time.time;
                SubtractTime(1);
                if (time <= 0)
                {
                    EndGame();
                }
            }
        }
    }

    void OnCannonTapped(CustomEventData data)
    {
        StartGame();
    }

    void OnImageTargetLost(CustomEventData data)
    {
        ExitGame();
    }

    void OnTargetHit(CustomEventData data)
    {
        hits++;
        TargetBehaviour tb = data.obj.GetComponent<TargetBehaviour>();
        int pointValue = tb.pointValue;
        int timeValue = tb.timeValueInSeconds;
        AddPoints(pointValue);
        AddTime(timeValue);
    }

    void OnShotTaken(CustomEventData data)
    {
        shots++;
    }

    public void OnReplayClicked()
    {
        StartGame();
    }

    public void OnExitClicked()
    {
        ExitGame();

        // Emit event
        CustomEventData data = new CustomEventData("onGameExit", gameObject);
        EventManager.TriggerEvent(data);
    }

    void StartGame()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
        hits = 0;
        shots = 0;
        SetPoints(0);
        SetTime(defaultTimeInSeconds);
        lastTime = Time.time;

        gameOverlay.SetActive(true);

        // Activate launch source

        cameraCannon.SetActive(true);
        launchSource.SetActive(true);

        startTime = Time.time;

        // Emit event

        CustomEventData data = new CustomEventData("onGameStart", gameObject);
        EventManager.TriggerEvent(data);

        gameInProgress = true;
    }

    // Game over behaviour
    void EndGame()
    {
        gameOver = true;
        int finalScore = score;
        int finalTime = Mathf.RoundToInt(Time.time - startTime);
        float accuracy = Mathf.Round((float)hits / (float)shots * 100f);
        gameOverScore.text = finalScore.ToString();
        gameOverTime.text = finalTime.ToString();
        gameOverAccuracy.text = accuracy + "%";
        gameOverPanel.SetActive(true);

        // Emit event
        CustomEventData data = new CustomEventData("onGameOver", gameObject);
        EventManager.TriggerEvent(data);
    }

    // Game Exit behaviour
    void ExitGame()
    {
        gameOver = false;
        gameInProgress = false;

        // Deactivate Launch source 

        if (launchSource != null)
        {
            launchSource.SetActive(false);
        }

        if (cameraCannon != null)
        {
            cameraCannon.SetActive(false);
        }

        if (gameOverlay != null)
        {
            gameOverlay.SetActive(false);
        }

    }

    void SetPoints(int points)
    {
        score = points;
        scoreText.text = score.ToString();
    }

    void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    void SetTime(int timeInSeconds)
    {
        time = timeInSeconds;
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        timerText.text = minutes + ":" + seconds;
    }

    void AddTime(int timeInSeconds)
    {
        time += timeInSeconds;
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        timerText.text = minutes + ":" + seconds;
    }

    void SubtractTime(int timeInSeconds)
    {
        time -= timeInSeconds;
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        timerText.text = minutes + ":" + seconds;
    }

}