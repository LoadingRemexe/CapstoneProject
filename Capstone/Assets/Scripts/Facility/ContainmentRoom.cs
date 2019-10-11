using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainmentRoom : MonoBehaviour
{
    [SerializeField] public SecurityCamera securityCam;
    [SerializeField] public GameObject containedEntity = null;
    [SerializeField] public DoorBehavior containmentDoor;
    [SerializeField] public DoorBehavior observationDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            securityCam.camIsAwake = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == containedEntity)
        {
            FindObjectOfType<FacilityBuilding>().SetLockdown(true);
        }
        if (other.CompareTag("Player"))
        {
            securityCam.camIsAwake = false;
        }
    }
}
