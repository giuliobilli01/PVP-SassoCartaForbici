using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{  
    [SerializeField]
    private SlotManager slotManager;
    public int firstPlayerPoints;
    public int secondPlayerPoints;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayersPoints() {
        for(int position=3; position<=5; position++) {
            CardType player1Slot = slotManager.firstPlayerSlots[position].GetCardType();
            CardType player2Slot = slotManager.secondPlayerSlots[position].GetCardType();

            if ((player1Slot != player2Slot)) {
                if (IsTheWinner(player1Slot, player2Slot)) {
                    this.firstPlayerPoints++;
                }else {
                    this.secondPlayerPoints++;
                }
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
