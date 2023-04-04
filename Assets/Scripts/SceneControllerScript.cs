using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    public SlotManager slotManager;
    public SceneControl sceneControl;

    public void ResetScene() {
        sceneControl.Reset();
        slotManager.ResetSlotPositions();
    }

    public void GoToMenu() {
        sceneControl.Load();
    }
}
