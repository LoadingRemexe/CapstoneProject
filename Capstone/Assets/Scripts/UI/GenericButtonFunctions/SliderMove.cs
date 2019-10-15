using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderMove : MonoBehaviour
{
    [SerializeField] Transform Slider = null;
    [SerializeField] public bool leftRight = true;

    public float Value = 0.5f;

    bool active = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && active)
        {
            if (leftRight)
            {
                Slider.localPosition += new Vector3(Input.GetAxis("Mouse X") / 10, 0, 0);
            }
            else
            {
                Slider.localPosition += new Vector3(Input.GetAxis("Mouse Y") / 10, 0, 0);
            }
            Slider.localPosition = new Vector3(Mathf.Clamp(Slider.localPosition.x, -1.3f, 1.3f), Slider.localPosition.y, Slider.localPosition.z);

        }
        if (!Input.GetMouseButton(0) && active)
        {
            active = false;
        }
        Value = (Slider.localPosition.x / 1.3f)/2 + 0.5f;
       // Debug.Log("Value = " +SliderValue);
    }

    public void ClickDown()
    {
        active = true;
    }
}
