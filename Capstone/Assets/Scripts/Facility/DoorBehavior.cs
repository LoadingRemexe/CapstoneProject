using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] AudioSource DoorSFX;
    [SerializeField] AudioSource DoorLockedSFX;
    public bool isUnlocked = true;
    public bool isOpen = false;
    Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", isOpen);
    }

    void Update()
    {
        animator.SetBool("isOpen", isOpen);

    }
    public void InvertLock()
    {
        isUnlocked = !isUnlocked;
        if (DoorLockedSFX) DoorSFX.Play();
    }

    public void InvertOpen()
    {
        if (isUnlocked)
        {
            if (DoorSFX) DoorSFX.Play();
            isOpen = !isOpen;
        } else
        {
            if (DoorLockedSFX) DoorSFX.Play();
        }
    }

    public void OpenDoor()
    {
        if (isUnlocked)
        {
            if (DoorSFX) DoorSFX.Play();
            isOpen = true;
        }
        else
        {
            if (DoorLockedSFX) DoorSFX.Play();
        }
    }
    public void CloseDoor()
    {
        if (DoorSFX) DoorSFX.Play();
        isOpen = false;
    }

    public void LockDoor()
    {
        isUnlocked = false;
        if (DoorLockedSFX) DoorSFX.Play();
    }
    public void UnlockDoor()
    {
        isUnlocked = true;
        if (DoorLockedSFX) DoorSFX.Play();
    }
}
