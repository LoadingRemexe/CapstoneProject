using UnityEngine;

public class ParaBearBabySight : StateMachineBehaviour
{
    ParaBearController pbc;
    GameObject targetBaby = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
  //      Debug.Log("Baby Sight Mode");

        pbc = animator.GetComponent<ParaBearController>();

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Baby");

        foreach (GameObject t in targets)
        {
            Vector3 direction = t.transform.position - animator.transform.position;
            Ray ray = new Ray(animator.transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Baby"))
                {
                    if (targetBaby == null || Vector3.Distance(pbc.navMeshAgent.transform.position, t.transform.position) < Vector3.Distance(pbc.navMeshAgent.transform.position, targetBaby.transform.position))
                    {
                        targetBaby = t;
                    }
                }
            }
        }
        if (targetBaby)
        {
            Debug.Log("Baby Targeted");
            pbc.navMeshAgent.SetDestination(targetBaby.transform.position);
        }
        else
        {
            Debug.Log("Baby Target Aborted");
            animator.SetTrigger("Idle");
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (targetBaby)
        {
            Vector3 direction = targetBaby.transform.position - animator.transform.position;
            Ray ray = new Ray(animator.transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.CompareTag("Baby"))
                {
                    targetBaby = null;
                }
            }
        }
        if (targetBaby)
        {
            pbc.navMeshAgent.SetDestination(targetBaby.transform.position);
        }
        else
        {
            //Debug.Log("Baby Sight Lost");
            animator.SetTrigger("Idle");
        }
         
        if (targetBaby)
        {
            if (Vector3.Distance(pbc.navMeshAgent.transform.position, targetBaby.transform.position) < 0.25f && !targetBaby.GetComponent<Carryable>().isHeld) //check if it has reached the baby, and the baby isnt being held onto by player or other creature.
            {
                Destroy(targetBaby);// destroy original 
                pbc.navMeshAgent.SetDestination(pbc.transform.position);
                animator.SetTrigger("Eat"); // animation has a fake baby to eat
                targetBaby = null;
                //Debug.Log("Baby Eaten");
                pbc.BabesConsumed++;
                pbc.ResetHunger();
                animator.SetTrigger("Idle");
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
