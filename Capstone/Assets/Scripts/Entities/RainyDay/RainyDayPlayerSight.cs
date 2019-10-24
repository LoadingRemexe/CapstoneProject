using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayPlayerSight : StateMachineBehaviour
{
    RainyDayController rdc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rdc = animator.GetComponent<RainyDayController>();
        animator.SetBool("HoldOutUmbrella", true);
        rdc.UmbrellaAnimator.SetBool("isOpen", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!rdc.CheckForPlayerSight()) animator.SetTrigger("Idle");

        Vector3 direction = rdc.playerMove.transform.position - rdc.transform.position;
        animator.transform.rotation = Quaternion.RotateTowards(rdc.transform.rotation,Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), 60.0f * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("HoldOutUmbrella", false);
        rdc.UmbrellaAnimator.SetBool("isOpen", false);
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
