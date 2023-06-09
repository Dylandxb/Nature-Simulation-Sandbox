using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{
    private MovementSM movementSM;
    public bool isWalking;
    public static bool hasReached;
    Vector3 target = new Vector3(-20.18f, 0.93f, 40.4f);
    public Walk(StateControl stateControl) : base("Walk", stateControl)
    {
        movementSM = (MovementSM)stateControl;
    }

    public override void EnterState()
    {
        base.EnterState();
        movementSM.iconHandler.idleIcon.SetActive(false);
        isWalking = true;
    }

    public override void UpdateStateLogic()
    {
        base.UpdateStateLogic();
        if (Vector3.Distance(movementSM.transform.position, target) < 0.5f)
        {
            //Return to idle state when distance between npc transform and target meets condition
            isWalking = false;
            stateControl.NextState(((MovementSM)stateControl).idle);
            hasReached = true;
            UpdateAnimation();
            UpdateMovement();
            //Walk towards mine after being idle again
        }
    }

    public override void UpdateAnimation()
    {
        base.UpdateAnimation();
        movementSM.animator.SetBool("walking", false);

    }

    public override void UpdateMovement()
    {
        base.UpdateMovement();
        Vector3 targetPos = new Vector3(target.x, movementSM.transform.position.y, target.z);
        movementSM.transform.LookAt(targetPos);
        movementSM.agent.SetDestination(targetPos);
        //Move to a waypoint. Set to Idle, turn around move back to waypoint. Set to idle and repeat
    }

}
