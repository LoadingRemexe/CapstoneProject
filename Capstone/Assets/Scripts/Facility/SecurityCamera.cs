using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
   [SerializeField] GameObject screen;
   [SerializeField] GameObject securityCam;

    public bool camIsAwake = false;
    public bool camIsFixed = true;

    Animator an;

    private void Start()
    {
        an = GetComponent<Animator>();
        an.SetBool("isFixed", camIsFixed);

    }

    void Update()
    {
        screen.SetActive(camIsAwake && camIsFixed);
        securityCam.SetActive(camIsAwake && camIsFixed);

        if (camIsFixed)
        {
        }
    }

    public void BreakCamera()
    {
        camIsFixed = false;
        an.SetBool("isFixed", camIsFixed);
    }

    public void FixCamera()
    {
        camIsFixed = true;
        an.SetBool("isFixed", camIsFixed);
    }


}
