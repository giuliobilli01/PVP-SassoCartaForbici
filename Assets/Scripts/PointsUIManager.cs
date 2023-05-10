using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PointsUIManager : MonoBehaviour
{

    [SerializeField]
    private  List<TMP_Text> pointsUI = new List<TMP_Text>();


    public void ShowPlayerPoints(int player, int points) {
        pointsUI[player].text = points.ToString();
    }

}
