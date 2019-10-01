using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyDayController : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody rb;
    CharacterController controller;
    public Animator KidAnimatorController = null;
    public Animator UmbrellaAnimatorController = null;
    public GameObject LhandTransform = null;
    public GameObject Umbrella = null;

    bool idle = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Umbrella.transform.position = LhandTransform.transform.position;
        Umbrella.transform.rotation = Quaternion.AngleAxis(90, LhandTransform.transform.forward) * Quaternion.AngleAxis(-90, LhandTransform.transform.right) * LhandTransform.transform.rotation;
    }


    void Awake()
    {
        StartCoroutine(RandomAction());
    }
    IEnumerator RandomAction()
    {
        while (idle)
        {

            float pause = Random.Range(30, 60);

            DoRandomAction();
            yield return new WaitForSeconds(pause);
        }
    }
    void DoRandomAction()
    {
        KidAnimatorController.SetTrigger("Action"+Random.Range(1, 4).ToString());
        //UmbrellaAnimatorController.SetTrigger(0);
    }
}

