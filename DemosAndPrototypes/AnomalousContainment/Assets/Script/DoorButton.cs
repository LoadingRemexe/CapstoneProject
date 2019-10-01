using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class DoorButton : MonoBehaviour
{
    public AudioSource sound = null;
    public Animator click = null;
    public OperationsManager OpManager;
    [SerializeField] VRInteractiveItem button = null;

    bool isOpen = false;

    private void OnEnable()
    {
        button.OnClick += ButtonClicked;
    }


    private void OnDisable()
    {
        button.OnClick -= ButtonClicked;
    }

    public void ButtonClicked()
    {
        if (click)
        {
            isOpen = !isOpen;
            click.SetBool("isOpen", isOpen);
        }
        if(sound) sound.Play();
    }

    public void Start()
    {
        OpManager = FindObjectOfType<OperationsManager>();
    }

    public void Update()
    {
      
    }
}
