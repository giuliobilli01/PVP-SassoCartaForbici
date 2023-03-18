using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour {
    
    public static List<Card> cardDatabase = new List<Card>();

    private static readonly int PAPER = 1;
    private static readonly int ROCK = 2;
    private static readonly int SCISSORS = 3;

    void Start() {

        DefaultInsert();
    }

    public static Card GetCard(int id) {
        return cardDatabase.Find(card => card.Id == id);
    }

    public static void InsertCard(int type) {
        cardDatabase.Add(new Card(type));
    }

    public static void DefaultInsert() {

        // player 1
        cardDatabase.Add(new Card(PAPER));
        cardDatabase.Add(new Card(ROCK));
        cardDatabase.Add(new Card(SCISSORS));

        cardDatabase.Add(new Card(PAPER));
        cardDatabase.Add(new Card(ROCK));
        cardDatabase.Add(new Card(SCISSORS));

        // player 2
        cardDatabase.Add(new Card(PAPER));
        cardDatabase.Add(new Card(ROCK));
        cardDatabase.Add(new Card(SCISSORS));

        cardDatabase.Add(new Card(PAPER));
        cardDatabase.Add(new Card(ROCK));
        cardDatabase.Add(new Card(SCISSORS));

    }



}
