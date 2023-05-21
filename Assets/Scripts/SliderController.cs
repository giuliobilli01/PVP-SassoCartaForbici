using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{   
    
    [SerializeField] RectTransform fxHolder;
    [SerializeField] Image sliderImage;
    [SerializeField] bool isLeftOrRight; // true if left slider, false if right slider

    private float timerProgress = 0;
    private float amountToFill;
    private float translatingPosition;

    // il timer va da 15 a 0 mentre il fillAmout da 0 a 1
    private void convertTimerToFillAmount(float timer) {
        this.amountToFill = timer / 15;
    }

    private void calculateInverseAmountToFill() {
        this.translatingPosition = 1 - amountToFill;
    }

    private void moveFxHolder() {
        
        if (isLeftOrRight) {
            // pos x from 0 to 700
            fxHolder.localPosition = new Vector3(-amountToFill * 700, 0, 0); 
        } else {
            // pos x from 0 to -700
            fxHolder.localPosition = new Vector3(amountToFill * 700, 0, 0);
        }
    }

    public void UpdateTimerProgress(float progress) {
        timerProgress = progress;
    } 

    /* void Start() {
        fxHolder.localPosition = new Vector3(0, 0, 0);
    } */

    void Update() {
        convertTimerToFillAmount(timerProgress);
        calculateInverseAmountToFill();
        sliderImage.fillAmount = amountToFill;
        moveFxHolder();
    }


}
