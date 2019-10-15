using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Toggle : MonoBehaviour
{
    [SerializeField] Animator click = null;
    [SerializeField] string animationName = "";

    public bool isOn = false;

    public void Start()
    {
        if (click)
        {
            click.SetBool(animationName, isOn);
        }
    }

    public void ButtonClicked()
    {
        isOn = !isOn;
        if (click)
        {
            click.SetBool(animationName, isOn);
        }
    }

    public void SetSwitch(bool onOff)
    {
        isOn = onOff;
        if (click)
        {
            click.SetBool(animationName, onOff);
        }
    }
}
