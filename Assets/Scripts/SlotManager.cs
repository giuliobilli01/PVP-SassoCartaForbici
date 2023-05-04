using System.Security.Authentication;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public List<Slot> firstPlayerSlots = new List<Slot>();
    public List<Slot> secondPlayerSlots = new List<Slot>();
    
    [SerializeField]
    private PointsManager pointsManager;

    public Transform firstPlayerSlotParent;
    public Transform secondPlayerSlotParent;

    public iTween.EaseType easeType;

    //  Fill the lists with the slots
    private void Awake() {
        foreach (Transform child in firstPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            slot.SetInitialPosition(child.position);
            slot.SetCurrentPosition(child.position);
            slot.SetInitialScale(child.localScale);
            slot.SetCurrentScale(child.localScale);
            firstPlayerSlots.Add(slot);
        }

        foreach (Transform child in secondPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            slot.SetInitialPosition(child.position);
            slot.SetCurrentPosition(child.position);
            slot.SetInitialScale(child.localScale);
            slot.SetCurrentScale(child.localScale);
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
            UpdateSlotsData(firstPlayerSlots);
        } else if (currentPlayer == 2) {
            SwapSlotsInList(secondPlayerSlots, firstSlot, secondSlot);
            SwapSlotObjects(firstSlot, secondSlot);
            UpdateSlotsData(secondPlayerSlots);
        }
        
        pointsManager.UpdatePlayersPoints(true, false);
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

        //iTween.MoveTo(secondSlotObject, iTween.Hash("position", firstSlot.GetCurrentPosition(), "time", 0.002f, "easetype", easeType));
        // fixed swap bug, but animation is not working now
        firstSlotObject.transform.position = secondSlot.GetCurrentPosition();
        firstSlotObject.transform.localScale = secondSlot.GetCurrentScale();

        secondSlotObject.transform.position = firstSlot.GetCurrentPosition();
        secondSlotObject.transform.localScale = firstSlot.GetCurrentScale();
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

    // update the new positions and scale of the slots
    public void UpdateSlotsData(List<Slot> slotList) {
        
        for (int i = 0; i < slotList.Count; i++) {
            slotList[i].SetCurrentPosition(slotList[i].transform.position);
            slotList[i].SetCurrentScale(slotList[i].transform.localScale);
        }
    }

    // reset the slots to their initial positions
    public void ResetSlotsData() {
        for (int i = 0; i < firstPlayerSlots.Count; i++) {
            firstPlayerSlots[i].transform.position = firstPlayerSlots[i].GetInitialPosition();
            firstPlayerSlots[i].SetCurrentPosition(firstPlayerSlots[i].GetInitialPosition());
            firstPlayerSlots[i].transform.localScale = firstPlayerSlots[i].GetInitialScale();
            firstPlayerSlots[i].SetCurrentScale(firstPlayerSlots[i].GetInitialScale());
        }

        for (int i = 0; i < secondPlayerSlots.Count; i++) {
            secondPlayerSlots[i].transform.position = secondPlayerSlots[i].GetInitialPosition();
            secondPlayerSlots[i].SetCurrentPosition(secondPlayerSlots[i].GetInitialPosition());
            secondPlayerSlots[i].transform.localScale = secondPlayerSlots[i].GetInitialScale();
            secondPlayerSlots[i].SetCurrentScale(secondPlayerSlots[i].GetInitialScale());
        }
    }

}
