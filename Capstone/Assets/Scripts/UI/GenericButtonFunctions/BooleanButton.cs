using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanButton : MonoBehaviour
{
    public bool isOn = false;
    public Animator click = null;
    public string animationName = "";

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
}
