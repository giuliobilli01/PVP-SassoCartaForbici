using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour, ISlot {
    
    // un unica lista con tutti gli slot di gioco

    public List<Slot> firstPlayerSlots = new List<Slot>();
    public List<Slot> secondPlayerSlots = new List<Slot>();

    public Transform firstPlayerSlotParent;
    public Transform secondPlayerSlotParent;

    // riempio la lista con tutti gli slot di gioco
    private void Awake() {
        foreach (Transform child in firstPlayerSlotParent.transform) {
            firstPlayerSlots.Add(child.GetComponent<Slot>());
        }

        foreach (Transform child in secondPlayerSlotParent.transform) {
            secondPlayerSlots.Add(child.GetComponent<Slot>());
        }
    }

    public void Swap() {
        
        // to-do
    }

}
