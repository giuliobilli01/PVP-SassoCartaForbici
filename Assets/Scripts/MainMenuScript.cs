using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
   [SerializeField]
   private SceneControl sceneControl;

   public void StartGame() {
        this.sceneControl.Load();
   }

   public void QuitGame() {
    this.sceneControl.Quit();
   }

    
}
