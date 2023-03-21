using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Card card;
    public SpriteRenderer spriteRenderer;
    public int id;

    void Awake()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init() {
        spriteRenderer.sprite = card.cardSprite;
        id = card.id;
    }
}
