using UnityEngine;

public class ParaBearIdle : StateMachineBehaviour
{
    ParaBearController pbc;

    float wanderDistance = 3.0f;
    float waitseconds = 5.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Idle Mode");

        pbc = animator.GetComponent<ParaBearController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (pbc.AreAnyBabiesInView())
        {
            animator.SetTrigger("BabySight");
        }
        else if (pbc.CheckForHunger())
        {
            animator.SetTrigger("BreakOut"); //if hungry, breakout
        }
        else if (pbc.CheckForPlayerSight())
        {
            animator.SetTrigger("PlayerSight"); // If not, then it follows player
        }

        waitseconds -= Time.deltaTime;

        if (waitseconds < 0.0f) // Random wander if neither are found
        {
            pbc.navMeshAgent.SetDestination(new Vector3(animator.transform.position.x + Random.Range(-wanderDistance, wanderDistance), 0.0f, animator.transform.position.z + Random.Range(-wanderDistance, wanderDistance)));
            //Debug.Log("Setting new Random Target");

            waitseconds = Random.Range(4, 16);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitseconds = 5.0f;
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
