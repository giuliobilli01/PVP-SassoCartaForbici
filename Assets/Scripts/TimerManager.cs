using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float remainingTime;
    public float countdownTime;
    
    [SerializeField]
    private TMP_Text timerText;
    
    [SerializeField]
    private TMP_Text countdownText;

    private bool timerIsRunning = false;

    private bool isTimerPassed = false;

    private bool countdownIsRunning = false;

    private bool isGameStarted = false;

    void Start() {
        timerText.enabled = false;
        countdownText.enabled = false;
    }

   
    void Update()
    {
        if (countdownIsRunning) {
    
            if (countdownTime > 0) {
                DisplayTime(countdownText, countdownTime);
                countdownTime -= Time.deltaTime;
            
            } else if(countdownTime <= 0 && countdownTime >= -1) {
                countdownText.text = "SWAP!";
                countdownTime -= Time.deltaTime;
            
            } else {
                countdownTime = -1;
                countdownIsRunning = false;
                countdownText.enabled = false;
                timerIsRunning = true;
                isGameStarted = true;
            }
            
        }

        if (timerIsRunning) {
            if (remainingTime > 0) {
                if (remainingTime <= 3) {
                    timerText.enabled = true;   
                }
                DisplayTime(timerText, remainingTime);
                remainingTime -= Time.deltaTime;
            }else {
                remainingTime = 0;
                timerIsRunning = false;
                isTimerPassed = true;
                timerText.enabled = false;
            
            }
        }
        
    }

    public void DisplayTime(TMP_Text text, float timeToDisplay) {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        text.text = string.Format("{0:0}", seconds);
    }

    public void StartTimer() {
        timerIsRunning = true;
    }

    public void StartCountdown() {
        countdownIsRunning = true;
        countdownText.enabled = true;
    }

    public bool IsTimerPassed() {
        return isTimerPassed;
    }

    public void SetIsTimerPassed(bool value){
        this.isTimerPassed=value;
    }

    public bool IsGameStarted() {
        return isGameStarted;
    }
}


