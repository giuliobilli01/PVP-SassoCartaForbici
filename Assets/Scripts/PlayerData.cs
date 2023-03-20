using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    public LinkedList<Card> deck = new LinkedList<Card>(); 
    public LinkedList<Card> table = new LinkedList<Card>();
    public int points;




   
}
