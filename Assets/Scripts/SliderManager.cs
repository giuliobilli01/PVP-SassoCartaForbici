using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
   [SerializeField]
   private SliderController slider1;

   [SerializeField]
   private SliderController slider2;

   public void updateSlider(float timerProgress) {
      slider1.UpdateTimerProgress(timerProgress);
      slider2.UpdateTimerProgress(timerProgress);
   }
}
