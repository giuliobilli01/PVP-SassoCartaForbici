using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    public SlotManager slotManager;

    public SceneControl sceneControl;

    public UIManager uIManager;

    public GameObject gameOverPanel;
    public GameObject circleLeft;
    public GameObject circleRight;


    public void ResetScene() {
        sceneControl.Reset();
        slotManager.ResetSlotsData();
    }

    public void GoToMenu() {
        sceneControl.Load();
    }

    public void GameOver() {
        uIManager.SetMatchResultText();
        gameOverPanel.SetActive(true);
        circleLeft.SetActive(false);
        circleRight.SetActive(false);
    }
}
