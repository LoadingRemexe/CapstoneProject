using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RainyDayController : MonoBehaviour
{
    [SerializeField] public Animator UmbrellaAnimator = null;
    [SerializeField] LayerMask entityLayer;
    [SerializeField] AudioSource footstep;
    [SerializeField] AudioSource UmbrellaSnap;
    [SerializeField] AudioSource[] Gurgles;

    public Animator animator { get; set; }
    public EntityBasic entityBasic { get; set; }
    public PlayerMove playerMove { get; set; }
    public NavMeshAgent navMeshAgent { get; set; }
    public Rigidbody rbody { get; set; }

    public void PlayFootstep()
    {
        footstep.Play();
    }
    public void PlayUmbrellaSnap()
    {
        UmbrellaSnap.Play();
    }
    public void PlayRandomGurgle()
    {
        Gurgles[Random.Range(0, Gurgles.Length)].Play();
    }

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
            "\nTime in Containment:" + (entityBasic.TimeInContainment/60).ToString("00.0") + " minutes" +
            "\nEmotional State: Happy(?)";
    }
}

