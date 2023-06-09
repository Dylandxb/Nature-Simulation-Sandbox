using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public AIState state;

    void Update()
    {
        //Checks for a next state every frame
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        //If not null then run current state
        AIState nextState = state?.RunCurrentState();

        if (nextState != null)
        {
            //Switch
            SwitchToState(nextState);
        }
    }

    public void SwitchToState(AIState nextState)
    {

        //Set new state to argument 
        state = nextState;

    }

    protected virtual AIState GetInitialState()
    {
        return null;
    }
}
