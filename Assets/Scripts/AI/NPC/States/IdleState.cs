using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : AIState
{
    //Idle is the initial state
    public DialogueState dialogueState;
    public static bool playerInDialogue;
    public static bool isIdle;
    public Animator animator;
    void Start()
    {
        //Player idle state begins, idle anim set to true
        animator.SetBool("idle", true);
    }
    //public IdleState(StateManager stateManager) : base(stateManager) { }

    private void Update()
    {
        //Static bool to check the state of the player interact is in dialogue
        playerInDialogue = NPCInteract.isInDialogue;

    }

    public override AIState RunCurrentState()
    {
        if (playerInDialogue)
        {
            //Calls the dialogue state
            return dialogueState;
        }
        else
        {
            //Remains in idle state
            isIdle = true;
            return this;
        }


    }
    private void OnTriggerEnter(Collider coll)
    {
        //Triggers to check for bool
        if(coll.CompareTag("Player"))
        {
            playerInDialogue = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
           playerInDialogue = false;
        }
    }
}
