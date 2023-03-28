using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot : MonoBehaviour {

    private Card card = new Card();

    [SerializeField] private CardType cardType;

    private Vector3 initialPosition;
    private Vector3 currentPosition;

    // awake
    private void Awake() {

        this.card.Initialize();
        this.card.SetCardType(this.cardType);
        Debug.Log(this.card.GetCardType());
    }

    public CardType GetCardType() {
        return this.card.GetCardType();
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


