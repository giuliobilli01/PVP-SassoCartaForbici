using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
    Paper,
    Rock,
    Scissors
}

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 1)]
public class CardData : ScriptableObject {

    // set card type
    public CardType cardType;

}
