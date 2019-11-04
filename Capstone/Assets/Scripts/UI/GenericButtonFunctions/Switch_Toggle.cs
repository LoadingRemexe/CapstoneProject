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

    public void SetSwitch(bool onOff)
    {
        isOn = onOff;
        if (click)
        {
            click.SetBool(animationName, onOff);
        }
    }
    public void InvertSwitch()
    {
        isOn = !isOn;
        if (click)
        {
            click.SetBool(animationName, isOn);
        }
    }
}