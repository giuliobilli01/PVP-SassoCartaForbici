using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public List<Slot> firstPlayerSlots = new List<Slot>();
    public List<Slot> secondPlayerSlots = new List<Slot>();

    public Transform firstPlayerSlotParent;
    public Transform secondPlayerSlotParent;

    //  Fill the lists with the slots
    private void Awake() {
        foreach (Transform child in firstPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            slot.SetInitialPosition(child.position);
            slot.SetCurrentPosition(child.position);
            firstPlayerSlots.Add(slot);
        }

        foreach (Transform child in secondPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            slot.SetInitialPosition(child.position);
            slot.SetCurrentPosition(child.position);
            secondPlayerSlots.Add(slot);
        }
    }

    // Swap handling logic
    // everytime a slot overlaps with another slot, this method is called
    // it checks which player the slots belong to and swaps them
    // it also saves the new positions of the slots
    public void Swap(Slot firstSlot, Slot secondSlot) {

        int currentPlayer = GetCurrentPlayer(firstSlot, secondSlot);

        if (currentPlayer == 1) {
            SwapSlotsInList(firstPlayerSlots, firstSlot, secondSlot);
            SwapSlotObjects(firstSlot, secondSlot);
        } else if (currentPlayer == 2) {
            SwapSlotsInList(secondPlayerSlots, firstSlot, secondSlot);
            SwapSlotObjects(firstSlot, secondSlot);
        }

        UpdateSlotPositions();
    }

    // swap the slots components in the list
    private void SwapSlotsInList(List<Slot> slotList, Slot firstSlot, Slot secondSlot) {
        
        int firstSlotIndex = slotList.IndexOf(firstSlot);
        int secondSlotIndex = slotList.IndexOf(secondSlot);

        Slot temp = slotList[firstSlotIndex];
        slotList[firstSlotIndex] = slotList[secondSlotIndex];
        slotList[secondSlotIndex] = temp;
        
    }

    // swap the slots objects in the scene
    private void SwapSlotObjects(Slot firstSlot, Slot secondSlot) {

        GameObject firstSlotObject = firstSlot.gameObject;
        GameObject secondSlotObject = secondSlot.gameObject;

        firstSlotObject.transform.position = secondSlot.GetCurrentPosition();
        iTween.MoveTo(secondSlotObject, iTween.Hash("position", firstSlot.GetCurrentPosition(), "time", 0.5f, "easetype", iTween.EaseType.easeOutBack));
    }

    // check which player the slots belong to
    public int GetCurrentPlayer(Slot firstSlot, Slot secondSlot) {

        if (firstPlayerSlots.Contains(firstSlot) && firstPlayerSlots.Contains(secondSlot)) {
            return 1;
        } else if (secondPlayerSlots.Contains(firstSlot) && secondPlayerSlots.Contains(secondSlot)) {
            return 2;
        } else {
            return 0;
        }
    }

    // update the new positions of the slots
    public void UpdateSlotPositions() {
        
        for (int i = 0; i < firstPlayerSlots.Count; i++) {
            firstPlayerSlots[i].SetCurrentPosition(firstPlayerSlots[i].transform.position);
        }
        for (int i = 0; i < secondPlayerSlots.Count; i++) {
            secondPlayerSlots[i].SetCurrentPosition(secondPlayerSlots[i].transform.position);
        }
    }

    // reset the slots to their initial positions
    public void ResetSlotPosition() {
        for (int i = 0; i < firstPlayerSlots.Count; i++) {
            firstPlayerSlots[i].transform.position = firstPlayerSlots[i].GetInitialPosition();
            firstPlayerSlots[i].SetCurrentPosition(firstPlayerSlots[i].GetInitialPosition());
        }

        for (int i = 0; i < secondPlayerSlots.Count; i++) {
            secondPlayerSlots[i].transform.position = secondPlayerSlots[i].GetInitialPosition();
            secondPlayerSlots[i].SetCurrentPosition(secondPlayerSlots[i].GetInitialPosition());
        }
    }

}
