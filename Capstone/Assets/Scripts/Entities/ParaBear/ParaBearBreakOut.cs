using UnityEngine;

public class ParaBearBreakOut : StateMachineBehaviour
{
    ParaBearController pbc;
    [SerializeField] GameObject paraCopy = null;
    float containmentCountdown = 60;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("BreakOut Mode");

        pbc = animator.GetComponent<ParaBearController>();
        pbc.entityBasic.containmentRoom.securityCam.BreakCamera();
        for (int i = 0; i < pbc.BabesConsumed; i++)
        {
            Instantiate(paraCopy, animator.transform.position, animator.transform.rotation, null);
        }
        pbc.BabesConsumed = 0;
        if (pbc.entityBasic.TimeInContainment/60 > 5.0f)
        {
            containmentCountdown = 30.0f;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pbc.AreAnyBabiesInView()) animator.SetTrigger("BabySight"); // Is hungry, but doesnt follow player anymore

        containmentCountdown -= Time.deltaTime;
        if (containmentCountdown <= 0.0f)
        {
            animator.SetTrigger("Escape");
        }
        if (!pbc.CheckForHunger())
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containmentCountdown = 60.0f;
        if (pbc.entityBasic.TimeInContainment/60 > 5.0f)
        {
            containmentCountdown = 30.0f;
        }
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
