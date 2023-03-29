using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
   public TMP_Text matchResultText;

   void Awake() {
    matchResultText.enabled = false;
   }

   public void SetMatchResultText(MatchResults result) {
        matchResultText.enabled = true;
        if(result == MatchResults.Player1Win) {
            matchResultText.text = "PLAYER 1 WIN";
        }else if (result == MatchResults.Player2Win) {
            matchResultText.text = "PLAYER 2 WIN";
        }else {
            matchResultText.text = "DRAW";
        }
   }

   public void DisableText(TMP_Text text) {
    text.enabled = false;
   }
}
