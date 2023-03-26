using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// card controller

public class Card {
    
    private CardData cardData;

    public void Initialize() {
        cardData = ScriptableObject.CreateInstance<CardData>();
    }

    public void SetCardType(CardType cardType) {
        cardData.cardType = cardType;
    }

    public CardType GetCardType() {
        return cardData.cardType;
    }

    public void SetCardData(CardData cardData) {
        this.cardData = cardData;
    }
}
