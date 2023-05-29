using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPulseAnimation : MonoBehaviour {

    [SerializeField] private SlotManager slotManager;

    private Animator animator;
    private int tokenIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayPulseAnimation(Slot firstSlot, int currentPlayer) {

        // get the index of the slot that is not empty
        if (currentPlayer == 1) {
            tokenIndex = slotManager.firstPlayerSlots.IndexOf(firstSlot) - 3;
        } else if (currentPlayer == 2) {
            tokenIndex = slotManager.secondPlayerSlots.IndexOf(firstSlot) - 3;
        }

        // play the animation
        animator.SetInteger("TokenPulse", tokenIndex);
    } 
        
    public IEnumerator WaitAndStopAnimation() {
        yield return new WaitForSeconds(1f);
        animator.SetInteger("TokenPulse", -1);
        animator.SetTrigger("Default");
    }
}
