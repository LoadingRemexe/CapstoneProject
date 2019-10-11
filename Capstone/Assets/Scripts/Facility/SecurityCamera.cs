using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
   [SerializeField] GameObject screen;
   [SerializeField] GameObject securityCam;
   [SerializeField] GameObject scope;

    public bool camIsAwake = false;
    public bool camIsFixed = true;

    float turnDirection = -1.0f;

    void Update()
    {
        screen.SetActive(camIsAwake && camIsFixed);
        securityCam.SetActive(camIsAwake && camIsFixed);

        if (camIsAwake && camIsFixed)
        {
            // turn left and right
            if (scope.transform.localRotation.y > 0) turnDirection = -1.0f ;
            if (scope.transform.localRotation.y < -90) turnDirection = 1.0f;
          //  Quaternion.RotateTowards(scope.transform.rotation, Quaternion.LookRotation(scope.transform.right * turnDirection, scope.transform.up), 30 * Time.deltaTime);
        }
    }

    public void BreakCamera()
    {
        camIsFixed = false;
    }

    public void FixCamera()
    {
        camIsFixed = true;
    }

   
}
