using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingletonScript : MonoBehaviour
{
    public static GameDataSingletonScript Instance {get; private set;}
    public GameObject cardSlot1;
    public GameObject cardSlot2;

    public int numberOfPlayerSlots;


    public List<GameObject> Player1Table = new List<GameObject>();
    public List<GameObject> Player1Deck = new List<GameObject>();
    public List<GameObject> Player1TableSlot = new List<GameObject>();
    public List<GameObject> Player1DeckSlot = new List<GameObject>();

    
    public List<GameObject> Player2Table = new List<GameObject>();
    public List<GameObject> Player2Deck = new List<GameObject>();   
    public List<GameObject> Player2TableSlot = new List<GameObject>();
    public List<GameObject> Player2DeckSlot = new List<GameObject>();



    private void Awake() {
        if (Instance == null) {
            // Setto l'istanza corrente con la classe corrente
            Instance = this;
            InsertSlotInList(cardSlot1, 1);
            InsertSlotInList(cardSlot2, 2);
            
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    }

    void Start() {
    }

    private void InsertSlotInList(GameObject slot, int list) {
       for (int i=0; i<numberOfPlayerSlots; i++) {
            if (list == 1) {
               Player1TableSlot.Add(slot.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject);
               Player1DeckSlot.Add(slot.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject);
            }else {
               Player2TableSlot.Add(slot.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject);
               Player2DeckSlot.Add(slot.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject);
            }
       }
    }

}
