using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainmentRoom : MonoBehaviour
{
    [SerializeField] public SecurityCamera securityCam;
    [SerializeField] public GameObject containedEntity = null;
    [SerializeField] public int roomNumber;
    [SerializeField] bool Loaded;
    [SerializeField] float InitialTimer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Loaded) containedEntity.SetActive(true);

        FindObjectOfType<FacilityBuilding>().updateEntityStatus(roomNumber,Loaded);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Loaded && InitialTimer > 0.0f)
        {
            InitialTimer -= Time.deltaTime;
        }

        if (InitialTimer <= 0.0f && !Loaded)
        {
            Loaded = true;
            containedEntity.SetActive(true);
            FindObjectOfType<FacilityBuilding>().updateEntityStatus(roomNumber, Loaded);
            FindObjectOfType<HandHeldPrompter>().CriticalAlert("An Entity Has Been Delivered To Containment Room #" + roomNumber.ToString());
            InitialTimer = 10.0f;
        }
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
