using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchResults {
    Player1Win,

    Player2Win,

    Draw,
}

public class GameManager : MonoBehaviour {
    
    public PointsManager pointsManager;

    public TimerManager timerManager;

    public UIManager uIManager;

    void Update() {
        if (timerManager.IsTimerPassed()) {
            pointsManager.UpdatePlayersPoints();
            timerManager.SetIsTimerPassed(false);
            uIManager.SetMatchResultText(pointsManager.GetMatchResult());
        }
    }

    

}
