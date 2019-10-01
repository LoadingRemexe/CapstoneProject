using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent InteractActivate = null;
    public GameObject OnHover = null;

    public void Start()
    {
        if (OnHover)
        {
            OnHover.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            OnHover.GetComponent<MeshRenderer>().receiveShadows = false;
            OnHover.SetActive(false);
        }

    }

    public void Interact()
    {
        if (InteractActivate != null)
        {
            InteractActivate.Invoke();
        }
    }

    void OnMouseEnter()
    {
        if(OnHover) OnHover.SetActive(true);
    }
    void OnMouseExit()
    {
        if (OnHover) OnHover.SetActive(false);
    }
}
