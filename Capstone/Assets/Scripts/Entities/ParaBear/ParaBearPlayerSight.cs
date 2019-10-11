using UnityEngine;

public class ParaBearPlayerSight : StateMachineBehaviour
{
    ParaBearController pbc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pbc = animator.GetComponent<ParaBearController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("PlayerSight Mode");

        if (pbc.AreAnyBabiesInView()) animator.SetTrigger("BabySight");
        pbc.CheckForHunger(); //if hungry, breakout


        Vector3 direction = pbc.playerMove.transform.position - animator.transform.position;
        Ray ray = new Ray(animator.transform.position + Vector3.up, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                animator.SetTrigger("Idle");
               // Debug.Log("Lost Sight Of Player");
            }
        }

        pbc.navMeshAgent.SetDestination(pbc.playerMove.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pbc.navMeshAgent.destination = pbc.navMeshAgent.transform.position;
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
