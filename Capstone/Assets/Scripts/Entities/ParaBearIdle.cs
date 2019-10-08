using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParaBearIdle : StateMachineBehaviour
{

    public float wanderDistance = 3.0f;
    
    float waitseconds = 5.0f;
    Animator an;
    PlayerMove pm;
    NavMeshAgent nma;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        an = animator.GetComponent<Animator>();
        pm = FindObjectOfType<PlayerMove>();
        nma = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = pm.transform.position - animator.transform.position;
        Ray ray = new Ray(animator.transform.position + Vector3.up, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                animator.SetTrigger("PlayerSight");
                Debug.Log("Sees Player");

            }
        }
        
        if (waitseconds < 0.0f)
        {
           nma.SetDestination(new Vector3(animator.transform.position.x + Random.Range(-wanderDistance, wanderDistance), 0.0f, animator.transform.position.z + Random.Range(-wanderDistance, wanderDistance)));



            Debug.Log("Setting new Random Target");

            waitseconds = Random.Range(4, 16);
        }
        if (nma.remainingDistance < 1.0f || nma.isStopped)
        {
            waitseconds -= Time.deltaTime;
        }
        animator.SetFloat("Speed", nma.speed);
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
