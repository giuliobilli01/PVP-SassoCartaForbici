using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
   public TMP_Text player1Result;

   public TMP_Text player2Result;

   public PointsUIManager pointsUIManager;

   public PointsManager pointsManager;

   public void SetMatchResultText() {
        MatchResults result = pointsManager.GetMatchResult();
        if(result == MatchResults.Player1Win) {
            player1Result.text = "YOU WIN!";
            player2Result.text = "YOU LOSE!";
        }else if (result == MatchResults.Player2Win) {
            player1Result.text = "YOU LOSE!";
            player2Result.text = "YOU WIN!";
        }else {
            player1Result.text = "DRAW!";
            player2Result.text = "DRAW!";
        }
   }

   public void DisableText(TMP_Text text) {
    text.enabled = false;
   }
}
