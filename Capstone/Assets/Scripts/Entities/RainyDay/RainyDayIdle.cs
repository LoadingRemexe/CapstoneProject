using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayIdle : StateMachineBehaviour
{
    RainyDayController rdc;
    float AnimTimer = 0.0f;
    float ViewTimer = 60.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimTimer = Random.Range(4.0f, 10.0f);
        rdc = animator.GetComponent<RainyDayController>();
        ViewTimer = 60.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rdc.CheckForPlayerSight()) animator.SetTrigger("PlayerSight");

        AnimTimer -= Time.deltaTime;
        if (AnimTimer <= 0.0f)
        {
            AnimTimer = Random.Range(4.0f, 10.0f);
            animator.SetTrigger("Action"+Random.Range(1, 4));
        }
        ViewTimer -= Time.deltaTime;
        if (ViewTimer <= 0.0f) // Hasnt been looked at for a while
        {
            animator.SetTrigger("BreakOut");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ViewTimer = 60.0f;

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
