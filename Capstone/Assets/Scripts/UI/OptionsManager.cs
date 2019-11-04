using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] SliderMove slider_Control1;
    [SerializeField] SliderMove slider_Control2;

    [SerializeField] TextMeshProUGUI audioReadout;
    [SerializeField] TextMeshProUGUI speedReadout;
    [SerializeField] TextMeshProUGUI cameraReadout;

    PlayerMove pm;
    float sensitivitymax = 40.0f;
    bool ControllerInput = false;
    float speedmax = 5.0f;
    float SFXVolume;


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMove>();
        slider_Control1.Value = pm.speed / speedmax;
        slider_Control2.Value = pm.cameraSmooth / sensitivitymax;
        audioMixer.GetFloat("SFX_Volume", out SFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(SFXVolume, -10, 20);
        audioMixer.SetFloat("SFX_Volume", SFXVolume);

        audioReadout.text = SFXVolume.ToString("0.00");

        pm.speed = slider_Control1.Value * speedmax;
        speedReadout.text = pm.speed.ToString("0.00");


        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapController();
        }
        pm.cameraSmooth = (ControllerInput) ? slider_Control2.Value * sensitivitymax : slider_Control2.Value * (12 / sensitivitymax);
        cameraReadout.text = pm.cameraSmooth.ToString("0.00");

    }

    public void IncreaseAudio()
    {
        SFXVolume++;
    }
    public void DecreaseAudio()
    {
        SFXVolume--;
    }

    public void SwapController()
    {
        ControllerInput = !ControllerInput;

    }


}
