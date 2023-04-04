using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SceneControl
{
    [SerializeField]
    private int sceneToLoad;

    public void Load() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit() {
        Application.Quit();
    }

    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
