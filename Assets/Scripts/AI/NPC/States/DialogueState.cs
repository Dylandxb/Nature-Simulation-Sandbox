using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueState : AIState
{
    public Animator animator;
    public IdleState idleState;
    public WanderState wanderState;
    public GuideState guideState;
    public static bool playerInDialogue;
    //public static bool npcIsGuiding;
    //public static bool npcIsWandering;
    public static bool beginGuide;
    private bool looking;
    public static bool beginWander;
    public Transform playerTarget;
    private float lookAtSpeed = 1.0f;
    public Button guide;
    public Button wander;
    public Button guideButton;
    public Button wanderButton;

    //Loads current dialogue state
    public override AIState RunCurrentState()
    {
        //Triggers interaction, modify the animation state
        animator.SetBool("talking", true);


        //Checks if the player is still in dialogue
        if (playerInDialogue == false)
        {
            //If not, change the animation back to idle and load the idle State
            animator.SetBool("talking", false);
            return idleState;
        }
        //When interacting with NPC always stop it and change it to idle state first
        if (beginGuide == true)
        {
            animator.SetBool("talking", false);
            Vector3 targetPos = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z);
            transform.parent.parent.LookAt(targetPos);
            return guideState;
        }
        if (beginWander == true)
        {
            animator.SetBool("talking", false);
            return wanderState;
        }
        else
        {
            //Else if player is still in dialogue return the dialogue state
            return this;

        }
    }

    public void StartWander()
    {
        //If clicked unfreeze player
        wander.interactable = false;
        wanderButton.interactable = false;
        beginWander = true;
    }
    public void StartGuide()
    {
        //Temporary fix
        guide.interactable = false;
        guideButton.interactable = false;
        beginGuide = true;
    }
    private void Update()
    {
        //Compare the two static bool states
        playerInDialogue = NPCInteract.isInDialogue;
        //beginGuide = GuideState.isGuiding;
        //beginWander = WanderState.isWandering;
        if(playerInDialogue)
        {
            RotateNPC();
        }
        else
        {
            looking = false;

            //FIX::When changing state dont look at player
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartGuide();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartWander();
        }
    }

    private void RotateNPC()
    {
        StartCoroutine(FacePlayer()); 
    }
    private IEnumerator FacePlayer()
    {
        //Current rotation of NPC
        //Gets the parents parent transform which is the NPC game object
        Quaternion currentRotate = transform.parent.parent.rotation;
        //Direction vector between the position of player to npc
        Quaternion lookRotate = Quaternion.LookRotation(playerTarget.position - transform.parent.parent.position);
        float timeToTurn = 0;
        //1 second to turn smoothly
        while (timeToTurn < 1.0f)
        {
            timeToTurn += Time.deltaTime * lookAtSpeed;
            //Lerp towards the vector between NPC and player
            transform.parent.parent.rotation = Quaternion.Lerp(currentRotate, lookRotate, timeToTurn);
            yield return null;
        }


    }


}
