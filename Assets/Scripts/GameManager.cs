using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MatchResults {
    Player1Win,
    Player2Win,
    Draw,
}

public class GameManager : MonoBehaviour {
    
    public PointsManager pointsManager;

    public TimerManager timerManager;

    public DragAndDrop dragManager;

    public SceneControllerScript sceneController;


    void Start() {
        timerManager.StartCountdown();
    }

    void Update() {

        if (timerManager.IsGameStarted()) {
            dragManager.enabled = true;
        } else {
            dragManager.enabled = false;
        }

        if (timerManager.IsTimerPassed()) {
            pointsManager.UpdatePlayersPoints(false, true);
            timerManager.SetIsTimerPassed(false);
            sceneController.GameOver();
        }
    }

    

}
