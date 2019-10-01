using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaBearController : MonoBehaviour
{
    public List<GameObject> eyes = null;

    public float speed = 5.0f;
    public float sightDistance = 5.0f;
    public float directionChangeInterval = 2;
    public float maxHeadingChange = 30;

    float waitseconds = -1.0f;
    Rigidbody rb;
    CharacterController controller;
    Vector3 m_velocity = Vector3.zero;
    float heading;
    Vector3 targetRotation;
    Animator an;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (waitseconds > -1.0f) waitseconds -= Time.deltaTime;
       foreach(GameObject g in eyes)
       {
           g.transform.LookAt(FindObjectOfType<Player>().transform.position + Vector3.up * 2.0f);
       }

        Vector3 direction = FindObjectOfType<Player>().transform.position - transform.position;

        Ray ray = new Ray(transform.position + Vector3.up, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightDistance))
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player && direction.magnitude > 1.0f)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * 2 * Time.deltaTime);
                direction.y -= 20.0f;
                controller.Move(direction.normalized * speed * Time.deltaTime);
            }
        }
        else if (waitseconds < 0.0f)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y -= 9.8f;
            controller.Move(forward * speed * Time.deltaTime);
        }
      
        m_velocity = controller.velocity;
        an.SetFloat("Speed", m_velocity.magnitude);
        
    }

    void Awake()
    {
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }
    IEnumerator NewHeading()
    {
        while (true)
        {
            if (Random.Range(0, 6) == 0 && waitseconds < 0.0f)
            {
                waitseconds = Random.Range(2, 8);
            }
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}

