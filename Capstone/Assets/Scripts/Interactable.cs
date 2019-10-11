using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent InteractActivate = null;
    [SerializeField] GameObject OnHover = null;

    public void Start()
    {
        if (OnHover)
        {
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

    public void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, FindObjectOfType<PlayerMove>().transform.position) > FindObjectOfType<PlayerMove>().SightDistance)
        {
            if (OnHover) OnHover.SetActive(false);
        }
    }
    private void OnMouseOver()
    {
        if (OnHover)
        {
            if (Vector3.Distance(gameObject.transform.position, FindObjectOfType<PlayerMove>().transform.position) <= FindObjectOfType<PlayerMove>().SightDistance) OnHover.SetActive(true);
            else OnHover.SetActive(false);
        }
    }
    void OnMouseEnter()
    {
        if (OnHover)
        {
            if (Vector3.Distance(gameObject.transform.position, FindObjectOfType<PlayerMove>().transform.position) <= FindObjectOfType<PlayerMove>().SightDistance) OnHover.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        if (OnHover) OnHover.SetActive(false);
    }
}
