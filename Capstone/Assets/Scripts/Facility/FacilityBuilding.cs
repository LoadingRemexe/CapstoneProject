using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
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
        Debug.Log("Lockdown set to " + lockdown);
    }
}
