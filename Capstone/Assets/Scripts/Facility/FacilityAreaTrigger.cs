using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityAreaTrigger : MonoBehaviour
{
   [SerializeField] public GameObject[] objects;

    void Start()
    {
        foreach (GameObject g in objects)
        {
            g.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject g in objects)
        {
            g.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject g in objects)
        {
            g.SetActive(false);
        }
    }
}
