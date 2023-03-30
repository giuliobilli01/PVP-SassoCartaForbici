using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
