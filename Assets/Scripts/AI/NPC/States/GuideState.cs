using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuideState : AIState
{
    public NavMeshAgent agent;
    public bool isGuiding;
    public static bool playerDialogue;
    public static bool isInIdle;
    public static bool canWander;
    public GameObject helloMsg;
    public IdleState idleState;
    public WanderState wanderState;
    public DialogueState dialogueState;
    public Animator animator;
    public Transform firstDestination;
    private bool reachedDest;
    [SerializeField] private GameObject destinationObj;
    public static Vector3 dest = new Vector3(-18.3f, 1.446f, -1.63f);
    private bool hasReached = false;

    private void Start()
    {
        destinationObj.transform.position = dest;
    }

    //disable player controllers to follow npc to set destination by itself
    public override AIState RunCurrentState()
    {
        //Moves to transform position destination
        MoveToDestination(dest);
        //animator.SetBool("walking", true);
        //Whilst player is guiding they can enter dialogue but remain in guide state after it
        if (reachedDest == true)
        {
            reachedDest = false;
            //isGuiding = false;
            canWander = true;
            isInIdle = true;
            animator.SetBool("walking", false);
            animator.SetBool("talking", false);
            //When reached destination stop agent
            agent.isStopped = true;
            return idleState;
        }
        //if(reachedDest == false)
        //{
        //    Debug.Log(isGuiding);
        //    animator.SetBool("walking", false);
        //    return this;
        //}
        if (playerDialogue)
        {
            isGuiding = false;
            Debug.Log(isGuiding);
            //Make is idle false
            isInIdle = false;
            agent.isStopped = true;
            return dialogueState;
        }
        if(canWander == true && isGuiding == false)
        {
            animator.SetBool("walking", true);
            return wanderState;
        }
        else
        {
            //Logic to determine if its not reached destination then remain in guiding state
            //if(reachedDest == false)
            //{
            //    isGuiding = true;

            //}
            //else
            //{
            //    animator.SetBool("walking", false);
            //    isInIdle = true;

            //}
            isGuiding = true;
            animator.SetBool("walking", true);
            return this;
        }

        //ISSUE IS PLAYER IS ALWAYS AT REACHED DEST (true) SO CONSTANTLY RETURN IDLE STATE
    }



    private void Update()
    {
        //Checks distance between NPC transform and its destination
        playerDialogue = NPCInteract.isInDialogue;
        isInIdle = IdleState.isIdle;
        canWander = DialogueState.beginWander;
        if (Vector3.Distance(transform.parent.parent.position, dest) < 2.0f)
        {
            reachedDest = true;
            //isGuiding = false;
            //Destroy(destinationObj);
            // StartCoroutine(Wait());
            //destinationObj.SetActive(false);

        }

        if (isGuiding)
        {
            //Face destination
            //Ignores looking at the Y position
            Vector3 targetPos = new Vector3(dest.x, transform.position.y, dest.z);
            transform.parent.parent.LookAt(targetPos);
            helloMsg.SetActive(false);

        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        reachedDest = false;

    }
    private void MoveToDestination(Vector3 destination)
    {
        //Controls the navmesh movement
        agent.isStopped = false;
        agent.enabled = true;
        agent.destination = destination;
        agent.SetDestination(destination);
    }

}
