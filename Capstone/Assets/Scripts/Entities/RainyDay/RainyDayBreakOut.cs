using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayBreakOut : StateMachineBehaviour
{
    RainyDayController rdc;
    float EscapeTimer = 60.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rdc = animator.GetComponent<RainyDayController>();
        rdc.UmbrellaAnimator.SetBool("isRaining",true);
        animator.SetBool("HoldUpUmbrella",true);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rdc.CheckForPlayerSight()) animator.SetTrigger("PlayerSight");

        EscapeTimer -= Time.deltaTime;
        if (EscapeTimer <= 0.0f) // Hasnt been looked at for a while
        {
            animator.SetTrigger("Escape");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rdc.UmbrellaAnimator.SetBool("isRaining", false);
        animator.SetBool("HoldUpUmbrella", false);
        EscapeTimer = 60.0f;

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
