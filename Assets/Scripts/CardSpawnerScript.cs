using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerScript : MonoBehaviour
{
    public GameObject paperCard;
    public GameObject rockCard;
    public GameObject scissorsCard;

    // Start is called before the first frame update
    void Start()
    {
        // Deve instanziare 4 oggetti per ogni carta nelle posizioni degli slot
        SpawnStartCards();

    }

    private void SpawnStartCards() 
    {
        DeckInitialization();
        TableInitialization();
    }

    private void DeckInitialization() {
        InsertAndInstantiate("deck", paperCard);
        InsertAndInstantiate("deck", rockCard);
        InsertAndInstantiate("deck", scissorsCard);
    }

    private void TableInitialization() {
        InsertAndInstantiate("table", paperCard);
        InsertAndInstantiate("table", rockCard);
        InsertAndInstantiate("table", scissorsCard);
    }

    private void InsertAndInstantiate(string position, GameObject card)
    {
        if (position == "deck") {
       GameDataSingletonScript.Instance.Player1Deck.Add(Instantiate(card, transform.position, transform.rotation));
       GameDataSingletonScript.Instance.Player2Deck.Add(Instantiate(card, transform.position, transform.rotation));

        } else if (position == "table") {
       GameDataSingletonScript.Instance.Player1Table.Add(Instantiate(card, transform.position, transform.rotation));
       GameDataSingletonScript.Instance.Player2Table.Add(Instantiate(card, transform.position, transform.rotation));
    
        }

    }
}
