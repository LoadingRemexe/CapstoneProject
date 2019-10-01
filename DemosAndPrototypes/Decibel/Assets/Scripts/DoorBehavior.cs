using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{

    public bool isUnlocked = true;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isUnlocked)
        {
            animator.SetBool("isOpen", true);
            Debug.Log("Open Door");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isUnlocked)
        {
            animator.SetBool("isOpen", false);
            Debug.Log("Close Door");

        }
    }

}
