using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMine : State
{
    private MovementSM movementSM;
    public bool isWalkingMine;
    public static bool hasReachedMine;
    Vector3 target = new Vector3(-69.99f, 1.23f, 66.67f);
    public WalkMine(StateControl stateControl) : base("WalkMine", stateControl)
    {
        movementSM = (MovementSM)stateControl;
    }

    public override void EnterState()
    {
        base.EnterState();
        movementSM.iconHandler.idleIcon.SetActive(false);
        isWalkingMine = true;
    }

    public override void UpdateStateLogic()
    {
        base.UpdateStateLogic();
        //Checks if distance between NPC and target
        if (Input.GetKeyDown(KeyCode.Alpha9) || Vector3.Distance(movementSM.transform.position, target) < 0.5f)
        {
            //Update logic, reset npc state back to Idle and update corresponding methods
            isWalkingMine = false;
            stateControl.NextState(((MovementSM)stateControl).idle);
            hasReachedMine = true;
            UpdateAnimation();
            UpdateMovement();
            //Walk towards mine after being idle again
        }
        if(StateControl.playerInRange && hasReachedMine)
        {
            stateControl.NextState(((MovementSM)stateControl).thank);
            
        }
    }

    public override void UpdateAnimation()
    {
        base.UpdateAnimation();
        movementSM.animator.SetBool("walking", false);

    }
    public override void UpdateMovement()
    {
        //Method inheritted from base class, used to find target destination for nav mesh agent to walk to
        base.UpdateMovement();
        Vector3 targetPos = new Vector3(target.x, movementSM.transform.position.y, target.z);
        movementSM.transform.LookAt(targetPos);
        movementSM.agent.SetDestination(targetPos);
        //Move to a waypoint. Set to Idle, turn around move back to waypoint. Set to idle and repeat
    }

}
