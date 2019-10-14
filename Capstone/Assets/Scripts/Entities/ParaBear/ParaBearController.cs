using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParaBearController : MonoBehaviour
{
    [SerializeField] public ContainmentRoom containmentRoom;
    [SerializeField] List<GameObject> eyes = null;
    public Animator animator;
    public PlayerMove playerMove;
    public NavMeshAgent navMeshAgent;

    public int BabesConsumed = 0;
    public float Hunger = 0.0f;
    public float HungerTime = 60;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = FindObjectOfType<PlayerMove>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        foreach (GameObject g in eyes)
        {
            g.transform.LookAt(playerMove.transform.position + Vector3.up * 2.0f);
        }
        Hunger += Time.deltaTime;

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    public void CheckForHunger()
    {
        if (Hunger >= HungerTime)
        {
            animator.SetTrigger("BreakOut");
        }
    }

    public void CheckForPlayerSight()
    {
        Vector3 direction = playerMove.transform.position - animator.transform.position;
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
    }

    public bool AreAnyBabiesInView()
    {
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
                    return true;
                }
            }
        }
        return false;
    }

    public void ResetHunger()
    {
        Hunger = 0.0f;
    }

    #region depreciated code


    //public List<GameObject> eyes = null;

    ////public float speed = 5.0f;
    ////public float sightDistance = 5.0f;

    ////float waitseconds = -1.0f;
    ////Rigidbody rb;

    ////Animator an;
    //PlayerMove pm;
    ////Vector3 target;
    ////NavMeshAgent nma;

    //void Start()
    //{
    //    //   rb = GetComponent<Rigidbody>();
    //    //   an = GetComponent<Animator>();
    //    pm = FindObjectOfType<PlayerMove>();
    //    //   nma = GetComponent<NavMeshAgent>();
    //}

    //void Update()
    //{
    //    //  if (waitseconds > -1.0f) waitseconds -= Time.deltaTime;
    //    foreach (GameObject g in eyes)
    //    {
    //        g.transform.LookAt(pm.transform.position + Vector3.up * 2.0f);
    //    }

    //    /* Vector3 direction = pm.transform.position - transform.position;

    //     Ray ray = new Ray(transform.position + Vector3.up, direction);
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit, sightDistance))
    //     {
    //         if (hit.collider.CompareTag("Player") && direction.magnitude > 1.0f)
    //         {
    //             target = pm.transform.position;
    //         }
    //     }
    //     else if (waitseconds < 0.0f)
    //     {
    //         target = new Vector3(transform.position.x + Random.Range(-sightDistance/2, sightDistance/2), 0.0f, transform.position.z + Random.Range(-sightDistance/2, sightDistance/2));
    //         Debug.Log("Setting new Random Target");

    //         waitseconds = Random.Range(4, 16);
    //     }
    //   */
    //}
    ///*
    //private void FixedUpdate()
    //{
    //    if (Vector3.Distance(target, transform.position) > 1)
    //    {
    //        var q = Quaternion.LookRotation(target - transform.position);
    //        rb.MovePosition(rb.transform.position += rb.transform.forward * speed * Time.deltaTime);
    //        rb.MoveRotation(Quaternion.Lerp(transform.rotation, q, 10 * Time.deltaTime));
    //        an.SetFloat("Speed", 1.0f);
    //    }
    //    else
    //    {
    //        an.SetFloat("Speed", 0.0f);
    //    }
    //}*/



    #endregion
}

