using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Idle : State
{
    public bool isIdle;
    private MovementSM movementSM;


    public Idle(StateControl stateControl) : base("Idle", stateControl)
    {
        movementSM = (MovementSM)stateControl;

    }

    public override void EnterState()
    {
        //Enter state sets variables to true to give a visual of idle
        base.EnterState();
        movementSM.iconHandler.idleIcon.SetActive(true);
        isIdle = true;
    }
    public override void UpdateStateLogic()
    {
        base.UpdateStateLogic();
        //Checks if player is in the NPC range and has not yet reached the first destination from Walk state
        if (StateControl.playerInRange == true && Walk.hasReached == false)
        {
            //Begins transition to next state (walk)
            //Updates the required anim
            isIdle = false;
            stateControl.NextState(((MovementSM)stateControl).walk);
            UpdateAnimation();
        }

        //Check if has reached is true then walk to Mine and go to next state
        if (StateControl.playerInRange == true && Walk.hasReached == true)
        {
            isIdle = false;
            stateControl.NextState(((MovementSM)stateControl).walkMine);
            UpdateAnimation();
        }
        if(WalkMine.hasReachedMine == true)
        {
            //Check for static bool in walkMine state, if true then disable walk anim
            movementSM.animator.SetBool("walking", false);
        }
    }

    public override void UpdateAnimation()
    {
        base.UpdateAnimation();
        movementSM.animator.SetBool("walking", true);

    }
}
