using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public PointsManager pointsManager;
    public TimerManager timerManager;

    void Update() {
        if (timerManager.IsTimerPassed()) {
            pointsManager.UpdatePlayersPoints();
            timerManager.SetIsTimerPassed(false);
        }
    }

    

}
