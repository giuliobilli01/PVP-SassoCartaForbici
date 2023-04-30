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

    public DragAndDrop dragManager;

    public AnalyticsManager analyticsManager;

    void Update() {

        if (timerManager.IsGameStarted()) {
            dragManager.enabled = true;
        } else {
            dragManager.enabled = false;
        }

        if (timerManager.IsTimerPassed()) {
            pointsManager.UpdatePlayersPoints(false, true);
            timerManager.SetIsTimerPassed(false);
            uIManager.SetMatchResultText(pointsManager.GetMatchResult());

            analyticsManager.SendMatchResultAnalytics(pointsManager.GetMatchResult(), dragManager.GetPlayerSwaps(1), dragManager.GetPlayerSwaps(2));
        }
    }

}
