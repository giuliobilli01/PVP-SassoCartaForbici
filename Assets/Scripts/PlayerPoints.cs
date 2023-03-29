using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints
{
    private List<SlotStatus> playerSlotStatus;

    public PlayerPoints() {
        playerSlotStatus = new List<SlotStatus>();
        
        // Inizializzo gli status 
        for (int i=0; i<3; i++) {
            playerSlotStatus.Add(SlotStatus.Undefined);
        }
    }

    public void AddStatus(int index, SlotStatus status) {
        this.playerSlotStatus[index] = status;
    }

    public int GetNumberOfWin() {
        int playerWin = 0;
        for(int i=0; i<playerSlotStatus.Count; i++) {
            if (playerSlotStatus[i] == SlotStatus.Win) {
                playerWin++;
            }
        }  
        return playerWin;
    }
}
