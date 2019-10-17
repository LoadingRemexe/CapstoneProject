using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityLight : MonoBehaviour
{
   [SerializeField] public Light lightFixture;

    void Start()
    {
        lightFixture.enabled = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        lightFixture.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        lightFixture.enabled = false;
    }
}
