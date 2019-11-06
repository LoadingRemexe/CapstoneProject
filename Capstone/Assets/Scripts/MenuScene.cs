using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    [SerializeField] Rigidbody MenuCamera;
    [SerializeField] Waypoint Waypoint;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider audioLevels;
    [SerializeField] TextMeshProUGUI containmentTime;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LongestContainment"))
        {
            containmentTime.text = (PlayerPrefs.GetFloat("LongestContainment")/60.0f).ToString("00.00") + " minutes";
        } else
        {
            PlayerPrefs.SetFloat("LongestContainment", 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MenuCamera.transform.position += (( Waypoint.transform.position - MenuCamera.transform.position).normalized * Time.deltaTime);
        MenuCamera.transform.rotation=  Quaternion.RotateTowards(MenuCamera.transform.rotation, Quaternion.LookRotation(Waypoint.transform.position - MenuCamera.transform.position, Vector3.up), 30 * Time.deltaTime);
        if (Vector3.Distance(MenuCamera.transform.position,Waypoint.transform.position) < .05f)
        {
            Waypoint = Waypoint.NextWaypoint;
        }

        audioMixer.SetFloat("SFX_Volume", audioLevels.value);

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Facility");
    }
}
