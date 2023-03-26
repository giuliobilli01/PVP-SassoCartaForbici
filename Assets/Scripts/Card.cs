using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

// serializable class for card data
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {
    public int id; // 1 = carta, 2 = sasso, 3 = forbici
    public Sprite cardSprite;

}
