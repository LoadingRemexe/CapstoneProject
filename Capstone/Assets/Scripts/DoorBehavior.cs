using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public bool isUnlocked = true;
    public bool isOpen = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator) animator.SetBool("isOpen", isOpen);

    }

    void Update()
    {

    }
    public void InvertLock()
    {
        isUnlocked = !isUnlocked;
    }

    public void InvertOpen()
    {
        if (isUnlocked)
        {
            isOpen = !isOpen;
            if (animator) animator.SetBool("isOpen", isOpen);
        }
    }

    public void OpenDoor()
    {
        if (isUnlocked)
        {
            isOpen = true;
            if (animator) animator.SetBool("isOpen", isOpen);
        }
    }
    public void CloseDoor()
    {
        isOpen = false;
        if (animator) animator.SetBool("isOpen", isOpen);
    }

    public void LockDoor()
    {
        isUnlocked = false;
    }
    public void UnlockDoor()
    {
        isUnlocked = true;
    }
}
