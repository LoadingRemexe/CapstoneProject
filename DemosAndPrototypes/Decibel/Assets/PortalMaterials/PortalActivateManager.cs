using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivateManager : MonoBehaviour
{
    public GameObject[] group1 = null;

    public Collider triggerBox1 = null;

    public Transform player = null;
    // Update is called once per frame
    void Update()
    {
        if (triggerBox1.bounds.Contains(player.position))
        {
            foreach (GameObject go in group1)
            {
                go.SetActive(true);
            }
        }
        if (!triggerBox1.bounds.Contains(player.position))
        {
            foreach (GameObject go in group1)
            {
                go.SetActive(false);
            }
        }

    }
}
