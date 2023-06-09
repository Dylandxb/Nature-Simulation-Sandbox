using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thank : State
{
    private MovementSM movementSM;
    private bool isThanking;
    //Call base class constructor
    public Thank(StateControl stateControl) : base("Thank", stateControl)
    {
        movementSM = (MovementSM)stateControl;
    }
    public override void EnterState()
    {
        //Disable icon when entering state
        base.EnterState();
        movementSM.iconHandler.idleIcon.SetActive(false);
        isThanking = true;
    }
    public override void UpdateStateLogic()
    {
        base.UpdateStateLogic();
        UpdateAnimation();
    }
    public override void UpdateAnimation()
    {
        //Play current state animation
        base.UpdateAnimation();
        movementSM.animator.SetBool("thankful", true);

    }
}
