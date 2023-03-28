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
    private List<SlotStatus> firstPlayerPoints = new List<SlotStatus>();
    private List<SlotStatus> secondPlayerPoints = new List<SlotStatus>();

    void Awake() {
        for (int i=0; i<3; i++) {
            firstPlayerPoints.Add(SlotStatus.Undefined);
            secondPlayerPoints.Add(SlotStatus.Undefined);
        }
    }
    void Update()
    {
        
    }

    public void UpdatePlayersPoints() {
        for(int position=0; position<3; position++) {
            CardType player1Slot = slotManager.firstPlayerSlots[position + 3].GetCardType();
            CardType player2Slot = slotManager.secondPlayerSlots[position + 3].GetCardType();

            if ((player1Slot != player2Slot)) {
                if (IsTheWinner(player1Slot, player2Slot)) {

                    this.firstPlayerPoints[position] = SlotStatus.Win;
                    this.secondPlayerPoints[position] = SlotStatus.Lose;
                    pointsUIManager.SetTextByIndex(position, SlotStatus.Win);
                }else {

                    this.secondPlayerPoints[position] = SlotStatus.Win;
                    this.firstPlayerPoints[position] = SlotStatus.Lose;
                    pointsUIManager.SetTextByIndex(position, SlotStatus.Lose);
                }
            } else {
            
                    this.firstPlayerPoints[position] = SlotStatus.Draw;
                    this.secondPlayerPoints[position] = SlotStatus.Draw;
                    pointsUIManager.SetTextByIndex(position, SlotStatus.Draw);
            }
        }
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


}
