using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingletonScript : MonoBehaviour
{
    public static GameDataSingletonScript Instance {get; private set;}

    public List<GameObject> Player1Table = new List<GameObject>();
    
    public List<GameObject> Player2Table = new List<GameObject>();
    
    public List<GameObject> Player1Deck = new List<GameObject>();

    public List<GameObject> Player2Deck = new List<GameObject>();


    private void Awake() {
        if (Instance == null) {
            // Setto l'istanza corrente con la classe corrente
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    }

}
