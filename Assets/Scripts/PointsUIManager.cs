using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PointsUIManager : MonoBehaviour
{
    [SerializeField]
    private  List<TMP_Text> firstPlayerCardSlotsUI = new List<TMP_Text>();

    [SerializeField]
    private  List<TMP_Text> secondPlayerCardSlotsUI = new List<TMP_Text>();

    [SerializeField]
    private  List<TMP_Text> middleSlotsUI = new List<TMP_Text>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < firstPlayerCardSlotsUI.Count; i++) {
            firstPlayerCardSlotsUI[i].enabled = false;
            secondPlayerCardSlotsUI[i].enabled = false;
            middleSlotsUI[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextByIndex(int index, SlotStatus status) {
        print(index);
         firstPlayerCardSlotsUI[index].enabled = true;
         secondPlayerCardSlotsUI[index].enabled = true;

        if (status == SlotStatus.Win) {
            firstPlayerCardSlotsUI[index].text = "WIN";
            secondPlayerCardSlotsUI[index].text = "LOSE";

        }else if(status == SlotStatus.Lose) {
            firstPlayerCardSlotsUI[index].text = "LOSE";
            secondPlayerCardSlotsUI[index].text = "WIN";

        }else {
            firstPlayerCardSlotsUI[index].text = "DRAW";
            secondPlayerCardSlotsUI[index].text = "DRAW";
        }
    }

    public void SetMiddleTextByIndex(int index, SlotStatus status) {
        middleSlotsUI[index].enabled = true;
        if (status == SlotStatus.Win) {
            middleSlotsUI[index].text = "PLAYER 1 IS WINNING";
        } else if (status == SlotStatus.Lose) {
            middleSlotsUI[index].text = "PLAYER 2 IS WINNING";
        }
    }

    public void DisableMiddleText() {
        for (int i=0; i<middleSlotsUI.Count; i++) {
            middleSlotsUI[i].enabled = false;
        }
    }
}
