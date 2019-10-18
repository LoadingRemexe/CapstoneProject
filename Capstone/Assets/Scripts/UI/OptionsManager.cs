using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] DialMove dial_Control;
    [SerializeField] SliderMove slider_Control1;
    [SerializeField] SliderMove slider_Control2;

    [SerializeField] TextMeshProUGUI audioReadout;
    [SerializeField] TextMeshProUGUI speedReadout;
    [SerializeField] TextMeshProUGUI cameraReadout;
    
    PlayerMove pm;
    float sensitivitymax = 60.0f;
    float speedmax = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMove>();
        slider_Control1.Value = pm.speed / speedmax;
        slider_Control2.Value = pm.cameraSmooth / sensitivitymax;
        float dialValue;
        audioMixer.GetFloat("SFX_Volume", out dialValue);
        dial_Control.Value = dialValue / 100.0f;
    }

    void UpButtonClicked()
    {

    }
    void DownButtonClicked()
    {

    }


    // Update is called once per frame
    void Update()
    {
        audioMixer.SetFloat("SFX_Volume", (dial_Control.Value * 99.9f) + 0.1f);
        audioReadout.text = dial_Control.Value.ToString("0.00");

        pm.speed = slider_Control1.Value * speedmax;
        speedReadout.text = pm.speed.ToString("0.00");

        pm.cameraSmooth = slider_Control2.Value * sensitivitymax;
        cameraReadout.text = pm.cameraSmooth.ToString("0.00");

    }
}
