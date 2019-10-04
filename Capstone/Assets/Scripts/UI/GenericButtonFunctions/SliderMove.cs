using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderMove : MonoBehaviour
{
    public Transform Slider = null;

    public float SliderValue = 0.5f;
    public TextMeshProUGUI ValueReadout = null;

    bool active = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && active)
        {

            Slider.localPosition += new Vector3(-Input.GetAxis("Mouse X")/10, 0, 0);
            Slider.localPosition = new Vector3(Mathf.Clamp(Slider.localPosition.x, -1.3f, 1.3f), Slider.localPosition.y, Slider.localPosition.z);

        }
        if (!Input.GetMouseButton(0) && active)
        {
            active = false;
        }
        SliderValue = (Slider.localPosition.x / 1.3f)/2 + 0.5f;
       // Debug.Log("Value = " +SliderValue);

        if (ValueReadout)
        {
            ValueReadout.text = SliderValue.ToString("0.00");
        }
    }

    public void ClickDown()
    {
        active = true;
    }
}
