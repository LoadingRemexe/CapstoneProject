using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class SwitchSceneButton : MonoBehaviour
{
    public string sceneswitchname = null;
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
        OpManager.SwitchScene(sceneswitchname);
    }

    public void Start()
    {
        OpManager = FindObjectOfType<OperationsManager>();
    }

    public void Update()
    {
      
    }
}
