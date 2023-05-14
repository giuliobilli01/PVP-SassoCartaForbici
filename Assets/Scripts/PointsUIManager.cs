using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PointsUIManager : MonoBehaviour
{
    public Sprite drawImage;

    public Sprite player1WinImage;

    public Sprite player2WinImage;

    [SerializeField]
    private  List<TMP_Text> pointsUI = new List<TMP_Text>();
    
    [SerializeField]
    private List<Image> matchStatusUI = new List<Image>();

    public void ShowPlayerPoints(int player, int points) {
        pointsUI[player].text = points.ToString();
    }

    public void ShowMatchStatus(int position, int winner) {
        matchStatusUI[position].enabled = true;
        if (winner == 1) {
            matchStatusUI[position].sprite = player1WinImage;
        } else if (winner == 2) {
            matchStatusUI[position].sprite = player2WinImage;
        }else {
            matchStatusUI[position].sprite = drawImage;
        }
    }

}
