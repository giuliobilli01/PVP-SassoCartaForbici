using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    public SlotManager slotManager;

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        slotManager.ResetSlotPositions();
    }
}
