using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
    [SerializeField] public Switch_Toggle LockDownSwitch;
    public bool onLockdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLockdown(bool lockdown)
    {
        onLockdown = lockdown;
        Light[] lights = FindObjectsOfType<Light>();
        foreach(Light  l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }

    public void InvertLockdown()
    {
        onLockdown = !onLockdown;
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }
}
