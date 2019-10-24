using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RainyDayController : MonoBehaviour
{
    [SerializeField] public Animator UmbrellaAnimator = null;
    [SerializeField] LayerMask entityLayer;

    public Animator animator;
    public EntityBasic entityBasic;
    public PlayerMove playerMove;
    public NavMeshAgent navMeshAgent;
    public Rigidbody rbody;


    void Start()
    {
        animator = GetComponent<Animator>();
        entityBasic = GetComponent<EntityBasic>();
        playerMove = FindObjectOfType<PlayerMove>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        UmbrellaAnimator.transform.localPosition = Vector3.zero;
        UmbrellaAnimator.transform.localRotation = Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.forward);

        rbody.velocity = Vector3.zero;
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        articulateStatistics();
    }

    public bool CheckForPlayerSight()
    {
        Vector3 direction = playerMove.transform.position - transform.position;
        Ray ray = new Ray(transform.position + Vector3.up, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500.0f, entityLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
             //   Debug.Log("Sees Player");
                return true;
            }
        }
        return false;
    }

    void articulateStatistics()
    {
        entityBasic.Statistics = "Entity Scanned: Entity #4 and #5" +
            "\nTime in Containment:" + entityBasic.TimeInContainment.ToString("00.00") + " seconds" +
            "\nEmotional State: Happy(?)";
    }
}

