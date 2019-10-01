using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{


    public Transform playerAnchor = null;
    public Transform verticalReset = null;
    public Transform LeftTilt = null;
    public Transform RightTilt = null;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 thumbpos = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        if (thumbpos.x > 0.1f)
        {
            playerAnchor.transform.rotation = Quaternion.Lerp(RightTilt.rotation, playerAnchor.transform.rotation, 1.0f);
            playerAnchor.transform.position = Vector3.Lerp(RightTilt.position, playerAnchor.transform.position, 1.0f);
        } else if (thumbpos.x < -0.1f)
        {
            playerAnchor.transform.rotation = Quaternion.Lerp(LeftTilt.rotation, playerAnchor.transform.rotation, 1.0f);
            playerAnchor.transform.position = Vector3.Lerp(LeftTilt.position, playerAnchor.transform.position, 1.0f);
        }
        else
        {
            playerAnchor.transform.rotation = Quaternion.Lerp(verticalReset.rotation, playerAnchor.transform.rotation, 1.0f);
            playerAnchor.transform.position = Vector3.Lerp(verticalReset.position, playerAnchor.transform.position, 1.0f);
        }
    }
}
