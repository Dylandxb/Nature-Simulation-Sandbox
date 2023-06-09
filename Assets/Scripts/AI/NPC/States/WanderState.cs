using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : AIState
{
    public static bool isWandering;
    public static bool triggerDialogue;
    public static bool isInIdle;
    private bool moveToDest;
    public NavMeshAgent agent;
    public Animator animator;
    public IdleState idleState;
    public DialogueState dialogueState;
    Vector3 target;
    //Layer NPC can move on
    [SerializeField] LayerMask ground, player;
    private float wanderRange = 40.0f;

    public override AIState RunCurrentState()
    {
        Wander();
        if (isWandering == true)
        {
            animator.SetBool("walking", true);

            //triggerDialogue = false;
            //Disable interaction whilst in wander
            //Call the event, keeps NPC in an infinite Wander state
            EventManager.instance.StopListeningDialogue();
            return this;
        }
        if(triggerDialogue == true)
        {
            isWandering = false;
            agent.isStopped = true;
            agent.speed = 0;
            animator.SetBool("walking", false);
            animator.SetBool("talking", true);
            return dialogueState;
        }
        else
        {
            return idleState;
        }

    }
    void Update()
    {
        //isWandering = DialogueState.beginWander;
        isInIdle = IdleState.isIdle;
        triggerDialogue = NPCInteract.isInDialogue;
    }
    private void Wander()
    {
        if (!moveToDest)
        {
            //Find a new destination once its reached the current target set by movedDest
            FindDest();
        }
        if (moveToDest)
        {
            //agent.SetDestination(destinations[destIndex].position);
            agent.SetDestination(target);
            
        }
        if(Vector3.Distance(transform.position,target) < 10)
        {
            moveToDest = false;
        }
        isWandering = true;
    }

    private void FindDest()
    {
        //Picks random x and z positions to move to
        //Remains in same Y pos
        float z = Random.Range(-wanderRange, wanderRange);
        float x = Random.Range(-wanderRange, wanderRange);
        target = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        //if anything directly below and can move along navmesh
        if(Physics.Raycast(target,Vector3.down,ground))
        {
            //Trigger bool to begin move
            moveToDest = true;
        }
    }
}
