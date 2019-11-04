using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] AudioSource DoorSFX;
    public bool isUnlocked = true;
    public bool isOpen = false;
    Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", isOpen);
       Interactable[] i =  GetComponentsInChildren<Interactable>();
        foreach (Interactable item in i)
        {
            item.InteractActivate.AddListener(InvertOpen);
        }
    }

    void Update()
    {
        animator.SetBool("isOpen", isOpen);

    }
    public void InvertLock()
    {
        isUnlocked = !isUnlocked;
    }

    public void InvertOpen()
    {
        if (isUnlocked)
        {
            if (DoorSFX) DoorSFX.Play();
            isOpen = !isOpen;
        }
    }

    public void OpenDoor()
    {
        if (isUnlocked)
        {
            if (DoorSFX) DoorSFX.Play();
            isOpen = true;
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
    }
    public void UnlockDoor()
    {
        isUnlocked = true;
    }
}
