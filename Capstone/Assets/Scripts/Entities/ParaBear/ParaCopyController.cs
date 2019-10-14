using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParaCopyController : MonoBehaviour
{
    public List<GameObject> eyes = null;

    public GameObject Baby;
    public GameObject Particles;

    float wanderDistance = 3.0f;

    float waitseconds = 5.0f;
    NavMeshAgent nma;
    Animator an;
    PlayerMove pm { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        an = GetComponent<Animator>();
        pm = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in eyes)
        {
            g.transform.LookAt(pm.transform.position + Vector3.up * 2.0f);
        }
        waitseconds -= Time.deltaTime;

        if (waitseconds < 0.0f) // Random wander if neither are found
        {
            nma.SetDestination(new Vector3(an.transform.position.x + Random.Range(-wanderDistance, wanderDistance), 0.0f, an.transform.position.z + Random.Range(-wanderDistance, wanderDistance)));

            waitseconds = Random.Range(4, 16);
        }
        an.SetFloat("Speed", nma.velocity.magnitude);
    }

    public void PlayerTouched()
    {
        Destroy(gameObject);
        Instantiate(Baby, transform.position, transform.rotation, null);
        GameObject particles = Instantiate(Particles, transform.position, transform.rotation, null);
        Destroy(particles, 2.0f);
    }
}
