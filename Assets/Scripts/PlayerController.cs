using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        if (data != null) {
            LoadPlayer(data);
        }
        //print(data.deck.Last.Value.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadPlayer(PlayerData data) {
        for (int i = 1; i<=3; i++) {
            data.deck.AddFirst(CardDatabase.GetCard(i));
            data.table.AddFirst(CardDatabase.GetCard(i));
        }
        
    }
}
