using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = null;
    [SerializeField] DialMove dial_Control = null;
    [SerializeField] SliderMove slider_Control = null;
    [SerializeField] BooleanButton switch_Control = null;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  audioMixer.SetFloat("Ambience_Volume", slider_Control.SliderValue);
      //  audioMixer.SetFloat("SFX_Volume", dial_Control.DialValue);
      //  float MasterVolume = (switch_Control.isOn) ? 1.0f: 0.01f;
      //  audioMixer.SetFloat("Master_Volume", MasterVolume);
    }
}
