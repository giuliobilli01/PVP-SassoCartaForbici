using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

// serializable class for card data
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {
    
    private int id; // 1 = carta, 2 = sasso, 3 = forbici
    // private string name; -> ci serve davvero?
    // private int owner;

    public int Id { get => id; set => id = value; }
    // public string Name { get => name; set => name = value; }
    // public int Owner { get => owner; set => owner = value; }
    
    public Card(int Id) {
        this.id = Id;
    }


}
