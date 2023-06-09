using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWait : StateMachineBehaviour
{
    [SerializeField] private float timeToRandomize;
    [SerializeField] private int numberOfAnims;
    private bool isBored;
    private float waitTime;
    private int anim;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetToIdle();

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isBored == false)
        {
            waitTime += Time.deltaTime;
            if (waitTime > timeToRandomize && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isBored = true;
                anim = Random.Range(1, numberOfAnims + 1);
                anim = anim * 2 - 1;
                //Sets to closest default idle anim
                animator.SetFloat("BoredAnim", anim - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetToIdle();
        }
        animator.SetFloat("BoredAnim", anim, 0.25f, Time.deltaTime);

    }

    private void ResetToIdle()
    {
        if (isBored)
        {
            anim--;
        }
        isBored = false;
        waitTime = 0;
        //anim = 0;
    }

}
