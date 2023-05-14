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

    public SliderManager sliderManager;
    
    // [SerializeField]
    // private TMP_Text timerText;
    
    [SerializeField]
    private TMP_Text countdownText;

    private bool timerIsRunning = false;

    private bool isTimerPassed = false;

    private bool countdownIsRunning = false;

    private bool isGameStarted = false;

    [SerializeField] private Animator animator;

    void Start() {
        //timerText.enabled = false;
        countdownText.enabled = false;
    }

   
    void Update()
    {
        if (countdownIsRunning) {
    
            if (countdownTime > 0) {
                DisplayTime(countdownText, countdownTime);
                countdownTime -= Time.deltaTime;
            
            } else if(countdownTime <= 0 && countdownTime > -1) {
                countdownText.text = "SWAP!";
                countdownTime -= Time.deltaTime;
            } else {
                countdownText.enabled = false;
                countdownTime = -1;
                countdownIsRunning = false;
                timerIsRunning = true;
                isGameStarted = true;
            }
            
        }

        if (timerIsRunning) {
            if (remainingTime >= 0) {
                sliderManager.updateSlider(remainingTime);
                remainingTime -= Time.deltaTime;

                /* if (remainingTime <= 3) {
                    animator.SetTrigger("Pulse");
                } */

            }else {
                remainingTime = -1;
                timerIsRunning = false;
                isTimerPassed = true;
                isGameStarted = false;
                //timerText.enabled = false;
            
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


