using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

   [SerializeField] private SceneControl sceneControl;
   [SerializeField] private Animator animator;

   public void StartGame() {
      
      animator.SetTrigger("FadeStart");
      this.sceneControl.Load();
   }

   public void QuitGame() {
      this.sceneControl.Quit();
   }

    
}
