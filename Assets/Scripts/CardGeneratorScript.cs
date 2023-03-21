using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGeneratorScript : SlotContainer
{
   
    public Card GetCard(int id) {
        if (id == 1) return paper;
        
        else if(id == 2) return rock;
        
        else if(id == 3) return scissors; 
    
        return null;
        
    }
}
