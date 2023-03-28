using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public List<Slot> firstPlayerSlots = new List<Slot>();
    public List<Slot> secondPlayerSlots = new List<Slot>();

    public Transform firstPlayerSlotParent;
    public Transform secondPlayerSlotParent;

    // riempio la lista con tutti gli slot di gioco
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

    public void Swap(Slot firstSlot, Slot secondSlot) {

        int currentPlayer = GetCurrentPlayer(firstSlot, secondSlot);

        if (currentPlayer == 1) {
            SwapSlotsInList(firstPlayerSlots, firstSlot, secondSlot);
            SwapSlotObjects(firstSlot, secondSlot);
            SaveSlotPositions(firstPlayerSlots);
        } else if (currentPlayer == 2) {
            SwapSlotsInList(secondPlayerSlots, firstSlot, secondSlot);
            SwapSlotObjects(firstSlot, secondSlot);
            SaveSlotPositions(secondPlayerSlots);
        }
    }

    private void SwapSlotsInList(List<Slot> slotList, Slot firstSlot, Slot secondSlot) {
        
        int firstSlotIndex = slotList.IndexOf(firstSlot);
        int secondSlotIndex = slotList.IndexOf(secondSlot);

        Slot temp = slotList[firstSlotIndex];
        slotList[firstSlotIndex] = slotList[secondSlotIndex];
        slotList[secondSlotIndex] = temp;
    }

    private void SwapSlotObjects(Slot firstSlot, Slot secondSlot) {

        GameObject firstSlotObject = firstSlot.gameObject;
        GameObject secondSlotObject = secondSlot.gameObject;

        firstSlotObject.transform.position = secondSlot.GetCurrentPosition();
        secondSlotObject.transform.position = firstSlot.GetCurrentPosition();
    }

    private int GetCurrentPlayer(Slot firstSlot, Slot secondSlot) {

        if (firstPlayerSlots.Contains(firstSlot) && firstPlayerSlots.Contains(secondSlot)) {
            return 1;
        } else if (secondPlayerSlots.Contains(firstSlot) && secondPlayerSlots.Contains(secondSlot)) {
            return 2;
        } else {
            return 0;
        }
    }

    private void SaveSlotPositions(List<Slot> slotList) {
        for (int i = 0; i < slotList.Count; i++) {
            slotList[i].SetCurrentPosition(slotList[i].transform.position);
        }
    }

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
