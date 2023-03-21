using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float remainingTime;
    
    public TMP_Text timerText;

    private bool timerIsRunning = false;

    void Start() {
        timerText.enabled = false;
    }

   
    void Update()
    {
        if (timerIsRunning) {
            if (remainingTime > 0) {
                if (remainingTime <= 3) {
                    timerText.enabled = true;   
                }
                DisplayTime(remainingTime);
                remainingTime -= Time.deltaTime;
            }else {
                remainingTime = 0;
                timerIsRunning = false;
                timerText.enabled = false;
            
            }
        }
        
    }

    public void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:0}", seconds);
    }

    public void StartTimer() {
        timerIsRunning = true;
    }
}


