using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
    [SerializeField] public GameObject OnHover;
    PlayerMove pm;
    public void Start()
    {
        pm = FindObjectOfType<PlayerMove>();
            OnHover.SetActive(false);
    }

    public void Update()
    {
        if (pm.objectInSight != gameObject)
        {
           OnHover.SetActive(false);
        }
    }
}
