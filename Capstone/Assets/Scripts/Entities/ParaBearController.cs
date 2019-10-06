using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaBearController : MonoBehaviour
{
    public List<GameObject> eyes = null;

    public float speed = 5.0f;
    public float sightDistance = 5.0f;

    float waitseconds = -1.0f;
    Rigidbody rb;

    Animator an;
    PlayerMove pm;
    Vector3 target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        pm = FindObjectOfType<PlayerMove>();
    }

    void Update()
    {
        if (waitseconds > -1.0f) waitseconds -= Time.deltaTime;
       foreach(GameObject g in eyes)
       {
           g.transform.LookAt(pm.transform.position + Vector3.up * 2.0f);
       }

        Vector3 direction = pm.transform.position - transform.position;

        Ray ray = new Ray(transform.position + Vector3.up, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player") && direction.magnitude > 1.0f)
            {
                target = pm.transform.position;
            }
        }
        else if (waitseconds < 0.0f)
        {
            target = new Vector3(transform.position.x + Random.Range(-sightDistance, sightDistance), 0.0f, transform.position.z + Random.Range(-sightDistance, sightDistance));
            Debug.Log("Setting new Random Target");

            waitseconds = Random.Range(4, 16);
        }
      
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(target, transform.position) > 1)
        {
            var q = Quaternion.LookRotation(target - transform.position);
            rb.MovePosition(rb.transform.position += rb.transform.forward * speed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, q, 10 * Time.deltaTime));
            an.SetFloat("Speed", 1.0f);
        }
        else
        {
            an.SetFloat("Speed", 0.0f);
        }
    }
}

