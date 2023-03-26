using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : PlayerClass
{
    // Start is called before the first frame update
    void Start()
    {
        this.points = 0;
        this.numberOfSlots = GameDataSingletonScript.Instance.numberOfPlayerSlots;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Va chiamata quando si cambia la posizione di una carta e nella disposizione iniziale perchè
    // altrimenti è troppo dispendiosa
    public override void CheckTable()
    {
        


        }
    
}
