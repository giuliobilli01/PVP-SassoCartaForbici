using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{   
    private float timerProgress = 0;
    public Slider slider;

    public void UpdateTimerProgress(float progress) {
        timerProgress = progress;
        slider.value = timerProgress;
    }


}
