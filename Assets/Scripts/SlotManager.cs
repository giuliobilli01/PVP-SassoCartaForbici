using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public List<Slot> firstPlayerSlots = new List<Slot>();
    public List<Slot> secondPlayerSlots = new List<Slot>();
    
    [SerializeField]
    private PointsManager pointsManager;

    public Transform firstPlayerSlotParent;
    public Transform secondPlayerSlotParent;

    public bool parallaxEnabled = true;

    //  Fill the lists with the slots
    private void Awake() {
        foreach (Transform child in firstPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            InitializeSlot(slot, child);
            firstPlayerSlots.Add(slot);
        }

        foreach (Transform child in secondPlayerSlotParent.transform) {
            Slot slot = child.GetComponent<Slot>();
            InitializeSlot(slot, child);
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

        firstSlotObject.transform.position = secondSlot.GetCurrentPosition();
        firstSlotObject.transform.localScale = secondSlot.GetCurrentScale();
        firstSlotObject.transform.rotation = secondSlot.GetCurrentRotation();

        secondSlotObject.transform.position = firstSlot.GetCurrentPosition();
        secondSlotObject.transform.localScale = firstSlot.GetCurrentScale();
        secondSlotObject.transform.rotation = firstSlot.GetCurrentRotation();

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

    // initializing slots data
    public void InitializeSlot(Slot slot, Transform transform) {

        slot.SetInitialPosition(transform.position);
        slot.SetCurrentPosition(transform.position);
        slot.SetInitialScale(transform.localScale);
        slot.SetCurrentScale(transform.localScale);
        slot.SetInitialRotation(transform.rotation);
        slot.SetCurrentRotation(transform.rotation);

    }

    // update the new positions and scale of the slots
    public void UpdateSlotsData(List<Slot> slotList) {
        
        for (int i = 0; i < slotList.Count; i++) {
            slotList[i].SetCurrentPosition(slotList[i].transform.position);
            slotList[i].SetCurrentScale(slotList[i].transform.localScale);
            slotList[i].SetCurrentRotation(slotList[i].transform.rotation);
        }
    }

    // reset the slots to their initial positions
    public void ResetSlotsData() {
        for (int i = 0; i < firstPlayerSlots.Count; i++) {
            firstPlayerSlots[i].transform.position = firstPlayerSlots[i].GetInitialPosition();
            firstPlayerSlots[i].SetCurrentPosition(firstPlayerSlots[i].GetInitialPosition());
            firstPlayerSlots[i].transform.localScale = firstPlayerSlots[i].GetInitialScale();
            firstPlayerSlots[i].SetCurrentScale(firstPlayerSlots[i].GetInitialScale());
            firstPlayerSlots[i].transform.rotation = firstPlayerSlots[i].GetInitialRotation();
            firstPlayerSlots[i].SetCurrentRotation(firstPlayerSlots[i].GetInitialRotation());
        }

        for (int i = 0; i < secondPlayerSlots.Count; i++) {
            secondPlayerSlots[i].transform.position = secondPlayerSlots[i].GetInitialPosition();
            secondPlayerSlots[i].SetCurrentPosition(secondPlayerSlots[i].GetInitialPosition());
            secondPlayerSlots[i].transform.localScale = secondPlayerSlots[i].GetInitialScale();
            secondPlayerSlots[i].SetCurrentScale(secondPlayerSlots[i].GetInitialScale());
            secondPlayerSlots[i].transform.rotation = secondPlayerSlots[i].GetInitialRotation();
            secondPlayerSlots[i].SetCurrentRotation(secondPlayerSlots[i].GetInitialRotation());
        }
    }

    // reset the slots to their previous position -> snapback all the slots
    // used for handling game over
    public void SnapAllSlotsBack() {
        for (int i = 0; i < firstPlayerSlots.Count; i++) {
            firstPlayerSlots[i].GetComponent<SortingGroup>().sortingOrder = 0;
            firstPlayerSlots[i].transform.position = firstPlayerSlots[i].GetCurrentPosition();
            firstPlayerSlots[i].transform.localScale = firstPlayerSlots[i].GetCurrentScale();
            firstPlayerSlots[i].transform.rotation = firstPlayerSlots[i].GetCurrentRotation();
        }

        for (int i = 0; i < secondPlayerSlots.Count; i++) {
            secondPlayerSlots[i].GetComponent<SortingGroup>().sortingOrder = 0;
            secondPlayerSlots[i].transform.position = secondPlayerSlots[i].GetCurrentPosition();
            secondPlayerSlots[i].transform.localScale = secondPlayerSlots[i].GetCurrentScale();
            secondPlayerSlots[i].transform.rotation = secondPlayerSlots[i].GetCurrentRotation();
        }
    }

    // parallax feature
    private void StartCardParallax(GameObject selectedObject) {
        
        float maxRotationAngle = 10f;
        float rotationSpeed = 2f;

        float additionalRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        Quaternion rotation = Quaternion.Euler(0, additionalRotation, 0);

        selectedObject.transform.rotation = rotation;
    }

    private void EndCardParallax(GameObject selectedObject) {
        selectedObject.transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate() {

        int[] tableIndexes = new int[] {3, 4, 5};

        if (parallaxEnabled) {
            for (int i = 0; i < tableIndexes.Length; i++) {
                StartCardParallax(firstPlayerSlots[tableIndexes[i]].gameObject);
                StartCardParallax(secondPlayerSlots[tableIndexes[i]].gameObject);
            }
        } else {
            for (int i = 0; i < tableIndexes.Length; i++) {
                EndCardParallax(firstPlayerSlots[tableIndexes[i]].gameObject);
                EndCardParallax(secondPlayerSlots[tableIndexes[i]].gameObject);
            }
        }
        
    }
    
}
