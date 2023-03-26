using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISlot {

    void Swap();

}

public class Slot : MonoBehaviour {

    private Card card = new Card();
    
    [SerializeField] private CardType cardType;

    // awake
    private void Awake() {
        // set card type
        this.card.SetCardType(cardType);
    }

    public CardType GetCardType() {
        return cardType;
    }
    
}


