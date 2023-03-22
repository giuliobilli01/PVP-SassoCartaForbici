using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerScript : MonoBehaviour
{
    public List<GameObject> cardTypes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Deve instanziare 4 oggetti per ogni carta nelle posizioni degli slot
        SpawnStartCards();

    }

    private void SpawnStartCards() 
    {
        for(int i=0; i < cardTypes.Count; i++) {
            DeckInitialization(i);
            TableInitialization(i);
        }
       
    }

    private void DeckInitialization(int cardTypeIndex) {
        InsertAndInstantiate("deck", cardTypes[cardTypeIndex], cardTypeIndex);
    }

    private void TableInitialization(int cardTypeIndex) {
        InsertAndInstantiate("table", cardTypes[cardTypeIndex], cardTypeIndex);
    }

    private void InsertAndInstantiate(string position, GameObject cardType, int slotIndex)
    {
        if (position == "deck") {
            GameDataSingletonScript.Instance.Player1Deck.Add(SetupCard(Instantiate(cardType, transform.position, transform.rotation), 1, slotIndex, position));
            GameDataSingletonScript.Instance.Player2Deck.Add(SetupCard(Instantiate(cardType, transform.position, transform.rotation), 2, slotIndex, position));

        } else if (position == "table") {
       GameDataSingletonScript.Instance.Player1Table.Add(SetupCard(Instantiate(cardType, transform.position, transform.rotation), 1, slotIndex, position));
       GameDataSingletonScript.Instance.Player2Table.Add(SetupCard(Instantiate(cardType, transform.position, transform.rotation), 2, slotIndex, position));
    
        }

    }

    private GameObject SetupCard(GameObject card, int player, int slotIndex, string position) {
       CardController controller = card.GetComponent<CardController>();
       if (position == "deck") {
        if (player == 1) {
            controller.currentSlot = GameDataSingletonScript.Instance.Player1DeckSlot[slotIndex];
            card.transform.position = GameDataSingletonScript.Instance.Player1DeckSlot[slotIndex].transform.position;
            card.transform.localScale = GameDataSingletonScript.Instance.Player1DeckSlot[slotIndex].transform.localScale;
        }else {
            controller.currentSlot = GameDataSingletonScript.Instance.Player2DeckSlot[slotIndex];
            card.transform.position = GameDataSingletonScript.Instance.Player2DeckSlot[slotIndex].transform.position;
            card.transform.localScale = GameDataSingletonScript.Instance.Player2DeckSlot[slotIndex].transform.localScale;
         }
       } else if (position == "table") {
         if (player == 1) {
            controller.currentSlot = GameDataSingletonScript.Instance.Player1TableSlot[slotIndex];
            card.transform.position = GameDataSingletonScript.Instance.Player1TableSlot[slotIndex].transform.position;
            card.transform.localScale = GameDataSingletonScript.Instance.Player1TableSlot[slotIndex].transform.localScale;

        }else {
            controller.currentSlot = GameDataSingletonScript.Instance.Player2TableSlot[slotIndex];
            card.transform.position = GameDataSingletonScript.Instance.Player2TableSlot[slotIndex].transform.position;
            card.transform.localScale = GameDataSingletonScript.Instance.Player2TableSlot[slotIndex].transform.localScale;

         }
       }
       return card;
    }
}
