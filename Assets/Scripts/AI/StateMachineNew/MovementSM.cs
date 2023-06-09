using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementSM : StateControl
{
    [HideInInspector]
    public Idle idle;
    [HideInInspector]
    public Walk walk;
    [HideInInspector]
    public WalkMine walkMine;
    [HideInInspector]
    public Thank thank;
    //Holds all relevant attributes to NPC which are MonoBehaviour related
    public Animator animator;
    public NavMeshAgent agent;
    public Transform transform;
    public GameObject blockade;
    [SerializeField] public NPCIcon iconHandler;



    private void Awake()
    {
        //Initialize the new states
        idle = new Idle(this);
        walk = new Walk(this);
        walkMine = new WalkMine(this);
        thank = new Thank(this);
    }
    protected override State GetInitialState()
    {
        return idle;
    }




}
