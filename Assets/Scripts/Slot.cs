using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot : MonoBehaviour {

    private Card card = new Card();

    [SerializeField] private CardType cardType;

    private Vector3 initialPosition; // for resetting
    private Vector3 currentPosition; // for handling swap 

    // awake
    private void Awake() {

        this.card.Initialize();
        this.card.SetCardType(this.cardType);
    }

    public CardType GetCardType() {
        return this.card.GetCardType();
    }

    public void SetCardType(CardType cardType) {
        this.card.SetCardType(cardType);
    }

    public void SetCurrentPosition(Vector3 position) {
        this.currentPosition = position;
    }

    public Vector3 GetCurrentPosition() {
        return this.currentPosition;
    }

    public void SetInitialPosition(Vector3 position) {
        this.initialPosition = position;
    }

    public Vector3 GetInitialPosition() {
        return this.initialPosition;
    }

}


