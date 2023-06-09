using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string stateName;
    protected StateControl stateControl;
    //Constructor to define a derived class which is a state
    public State(string name, StateControl stateControl)
    {
        this.stateName = name;
        this.stateControl = stateControl;
    }
    //Methods exist here and are implemented in child classes
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateStateLogic() { stateControl.StartMove(); stateControl.StopMove(); }
    public virtual void UpdateAnimation() { }
    public virtual void UpdateMovement() { }

}
