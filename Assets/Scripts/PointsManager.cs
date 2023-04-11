using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotStatus {
    Win,
    Lose,
    Draw,
    Undefined,
}

public class PointsManager : MonoBehaviour
{  
    [SerializeField]
    private SlotManager slotManager;
    
    [SerializeField]
    private PointsUIManager pointsUIManager;
    private PlayerPoints player1Points;
    private PlayerPoints player2Points;


    void Awake() {
       player1Points = new PlayerPoints();
       player2Points = new PlayerPoints();

    }

    public void UpdatePlayersPoints(bool showCurrentStatus, bool showFinalStatus) {
        for(int position=0; position<3; position++) {
            CardType player1Slot = slotManager.firstPlayerSlots[position + 3].GetCardType();
            CardType player2Slot = slotManager.secondPlayerSlots[position + 3].GetCardType();

            SetPlayerPoints(player1Slot, player2Slot, position, showCurrentStatus, showFinalStatus);
        }
       Debug.Log(GetMatchResult());
    }

    private bool IsTheWinner(CardType player, CardType opponent) {
        if (player == CardType.Rock) {
            if (opponent == CardType.Paper) {
                return false;
            }else {
                return true;
            }
        }else if(player == CardType.Paper) {
             if (opponent == CardType.Scissors) {
                return false;
            }else {
                return true;
            }
        }else {
             if (opponent == CardType.Rock) {
                return false;
            }else {
                return true;
            }
        }
    } 
    public MatchResults GetMatchResult() {
        if (player1Points.GetNumberOfWin() > player2Points.GetNumberOfWin()) {
            return MatchResults.Player1Win;
        }else if (player1Points.GetNumberOfWin() < player2Points.GetNumberOfWin()) {
            return MatchResults.Player2Win;
        }else {
            return MatchResults.Draw;
        }
    }

    public void SetPlayerPoints(CardType player1Slot, CardType player2Slot, int position, bool showCurrentStatus, bool showFinalStatus) {
        if ((player1Slot != player2Slot)) {
                if (IsTheWinner(player1Slot, player2Slot)) {

                    player1Points.AddStatus(position, SlotStatus.Win);
                    player2Points.AddStatus(position, SlotStatus.Lose);
                    if (showFinalStatus) {
                        pointsUIManager.SetTextByIndex(position, SlotStatus.Win);
                    } else if (showCurrentStatus) {
                        pointsUIManager.SetMiddleTextByIndex(position,SlotStatus.Win);
                    }
                }else {

                    player1Points.AddStatus(position, SlotStatus.Lose);
                    player2Points.AddStatus(position, SlotStatus.Win);
                    if (showFinalStatus) {
                        pointsUIManager.SetTextByIndex(position, SlotStatus.Lose);
                    } else if (showCurrentStatus) {
                        pointsUIManager.SetMiddleTextByIndex(position, SlotStatus.Lose);
                    }
                }
            } else {
            
                    player1Points.AddStatus(position, SlotStatus.Draw);
                    player2Points.AddStatus(position, SlotStatus.Draw);
                    if (showFinalStatus) {
                        pointsUIManager.SetTextByIndex(position, SlotStatus.Draw);
                    } else if (showCurrentStatus) {
                        pointsUIManager.SetMiddleTextByIndex(position, SlotStatus.Draw);
                    }
            }
    }
}
