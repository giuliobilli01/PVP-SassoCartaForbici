using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PointsUIManager : MonoBehaviour
{
    [SerializeField]
    private  List<TMP_Text> firstPlayerSlotsUI = new List<TMP_Text>();

    [SerializeField]
    private  List<TMP_Text> secondPlayerSlotsUI = new List<TMP_Text>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < firstPlayerSlotsUI.Count; i++) {
            firstPlayerSlotsUI[i].enabled = false;
            secondPlayerSlotsUI[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextByIndex(int index, SlotStatus status) {
        print(index);
         firstPlayerSlotsUI[index].enabled = true;
         secondPlayerSlotsUI[index].enabled = true;

        if (status == SlotStatus.Win) {
            firstPlayerSlotsUI[index].text = "WIN";
            secondPlayerSlotsUI[index].text = "LOSE";

        }else if(status == SlotStatus.Lose) {
            firstPlayerSlotsUI[index].text = "LOSE";
            secondPlayerSlotsUI[index].text = "WIN";

        }else {
            firstPlayerSlotsUI[index].text = "DRAW";
            secondPlayerSlotsUI[index].text = "DRAW";
        }
    }
}
