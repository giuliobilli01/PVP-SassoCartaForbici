using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    
    public static List<Card> cardDatabase = new List<Card>();

    [SerializeField]
    private int eachTypeCardNumber; 

    private static int numberOfPlayers = 2;
    private static readonly int PAPER = 1;
    private static readonly int ROCK = 2;
    private static readonly int SCISSORS = 3;

    void Start() {

        DefaultInsert(eachTypeCardNumber);
    }

    public static Card GetCard(int id) {
        return cardDatabase.Find(card => card.Id == id);
    }

    public static void InsertCard(int type) {
        // Per creare un'istanza di uno ScriptableObject si utilizza CreateInstance
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.Id = type;
        cardDatabase.Add(newCard);
    }

    public static void DefaultInsert(int eachCardTypeNumber) {

        for (int i=0; i < eachCardTypeNumber * numberOfPlayers; i++) {
            InsertCard(PAPER);
            InsertCard(ROCK);
            InsertCard(SCISSORS);
        }


    }



}
