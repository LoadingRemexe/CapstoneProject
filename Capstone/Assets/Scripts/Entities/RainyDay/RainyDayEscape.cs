using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayEscape : StateMachineBehaviour
{
    RainyDayController rdc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rdc = animator.GetComponent<RainyDayController>();
        //Copied from parabear
        rdc.navMeshAgent.SetDestination(FindObjectOfType<FacilityBuilding>().Exit.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Copied from parabear
        Ray ray = new Ray(animator.transform.position, animator.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1.0f))
        {
            if (hit.collider.GetComponentInParent<DoorBehavior>())
            {
                animator.SetTrigger("Action2");
                //Open door if it finds it
                hit.collider.GetComponentInParent<DoorBehavior>().OpenDoor();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
