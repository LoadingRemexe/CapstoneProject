using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class PlaySoundButton : MonoBehaviour
{
    public AudioSource sound = null;
    public Animator click = null;
    public OperationsManager OpManager;
    [SerializeField] VRInteractiveItem button = null;

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
            click.SetTrigger("Click");
        }
        sound.Play();
    }

    public void Start()
    {
        OpManager = FindObjectOfType<OperationsManager>();
    }

    public void Update()
    {
      
    }
}
