using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] SliderMove slider_Control1;
    [SerializeField] SliderMove slider_Control2;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] TextMeshProUGUI audioReadout;
    [SerializeField] TextMeshProUGUI speedReadout;
    [SerializeField] TextMeshProUGUI cameraReadout;

    PlayerMove pm;
    Vector2 sensitivitymax = new Vector2(1.0f, 9.0f);
    float speedmax = 8.0f;
    float SFXVolume;


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMove>();
        slider_Control1.Value = pm.speed / speedmax;
        slider_Control2.Value = pm.cameraSmooth / sensitivitymax.y;
        audioMixer.GetFloat("SFX_Volume", out SFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(SFXVolume, -80, 0);
        audioMixer.SetFloat("SFX_Volume", SFXVolume);

        audioReadout.text = (((SFXVolume + 80.0f)/80)*100).ToString("00");

        pm.speed = slider_Control1.Value * speedmax;
        speedReadout.text = pm.speed.ToString("0.00");

        pm.cameraSmooth = sensitivitymax.x + (slider_Control2.Value * sensitivitymax.y) ;
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

    public void PauseGame()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void Exit()
    {
        //SceneManager.LoadScene("Menu");
      foreach(Scene scene in SceneManager.GetAllScenes())
        {
            if (scene != SceneManager.GetActiveScene())
            {

                Debug.Log("unloaded " + scene.name);
                SceneManager.UnloadScene(scene);
            }
        }
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync("Menu"));

    }


}
