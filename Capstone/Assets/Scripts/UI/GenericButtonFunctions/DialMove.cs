using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialMove : MonoBehaviour
{

    [SerializeField] Transform Dial = null;
    [SerializeField] float valueRate = 0.001f;

    [SerializeField] float DialValue = 0.5f;
    [SerializeField] TextMeshProUGUI ValueReadout= null;


    bool active = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && active)
        {
            Debug.Log(Input.GetAxis("Mouse X"));

            Dial.Rotate(0, (Input.GetAxis("Mouse X") *2), 0, Space.Self);

        }
        if (!Input.GetMouseButton(0) && active)
        {
            active = false;
        }
        DialValue =( (Dial.rotation.y / 3.6f)* 2 + 0.5f);

        DialValue = Mathf.Clamp01(DialValue);
        if (ValueReadout)
        {
            ValueReadout.text = DialValue.ToString("0.00");
        }
    }

    public void ClickDown()
    {
        active = true;
    }
}
