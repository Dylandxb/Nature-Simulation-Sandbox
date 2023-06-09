using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StateControl : MonoBehaviour
{
    State currentState;
    public static bool playerInRange;

    void Start()
    {
        playerInRange = false;
        //Set initial state to Idle
        currentState = GetInitialState();
        if(currentState != null)
        {
            //If a current state is found enter it
            currentState.EnterState();
        }
        EventManager.instance.NPCWalk += StartMove;
        EventManager.instance.NPCIdle += StopMove;
    }

    private void OnDisable()
    {
        EventManager.instance.NPCWalk -= StartMove;
        EventManager.instance.NPCIdle -= StopMove;

    }

    void Update()
    {
        if(currentState != null)
        {
            //Update all the logic of the new current state
            currentState.UpdateStateLogic();
        }
    }

    private void LateUpdate()
    {
        if (currentState != null)
        {
            //After logic update its movement
            currentState.UpdateMovement();
        }
    }

    public void NextState(State newState)
    {
        //Exit current state
        currentState.ExitState();
        //Assign new state
        currentState = newState;
        //Enter state
        currentState.EnterState();
    }

    protected virtual State GetInitialState()
    {
        return null;
    }

    public void StartMove()
    {
    }

    public void StopMove()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Set player in range of NPC to true when colliding
             playerInRange = true;
        }
        if (other.gameObject.CompareTag("Blockade"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
