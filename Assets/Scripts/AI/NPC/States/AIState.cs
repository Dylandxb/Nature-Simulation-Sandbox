using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    //Abstract class
    //Controls states
    public abstract AIState RunCurrentState();



}
