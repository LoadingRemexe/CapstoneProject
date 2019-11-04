using UnityEngine;
using UnityEngine.SceneManagement;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
    [SerializeField] public Switch_Toggle LockDownSwitch;
    [SerializeField] bool LoadParabear = false;
    [SerializeField] bool LoadRainyDay = false;
    [SerializeField] AudioSource RedAlert;
    public bool onLockdown;

    Light[] lights;


    float InitialTimer = 10.0f;
    bool loaded = false;
    PlayerMove playerMove;
    HandHeldPrompter playerPrompts;


    // Start is called before the first frame update
    void Start()
    {
        LockDownSwitch.SetSwitch(onLockdown);

        RedAlert.Play();
        RedAlert.Pause();
        playerMove = FindObjectOfType<PlayerMove>();
        playerPrompts = FindObjectOfType<HandHeldPrompter>();
        lights = FindObjectsOfType<Light>();

        string LoadInPrompts = "";
        LoadInPrompts += "Observation Room 1: \nVacant \n";
        if (LoadRainyDay)
        {
            LoadInPrompts += "Observation Room 2: \nEntity Arrived \n";
            SceneManager.LoadScene("RainyDayContainmentRoom", LoadSceneMode.Additive);
        }
        else
        {
            LoadInPrompts += "Observation Room 2: \nVacant \n";
            SceneManager.LoadScene("EmptyRainyDayContainmentRoom", LoadSceneMode.Additive);
        }
        LoadInPrompts += "Observation Room 3: \nVacant \n";
        if (LoadParabear)
        {
            LoadInPrompts += "Observation Room 4: \nEntity Arrived \n";
            SceneManager.LoadScene("ParaBearContainmentRoom", LoadSceneMode.Additive);
        }
        else
        {
            LoadInPrompts += "Observation Room 4: \nVacant \n";
            SceneManager.LoadScene("EmptyParaBearContainmentRoom", LoadSceneMode.Additive);
        }
        LoadInPrompts += "Observation Room 5: \nVacant \n";
        playerPrompts.UpdateText1(LoadInPrompts);

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EntityBasic eb = other.GetComponent<EntityBasic>();
        if (eb)
        {
            SceneManager.LoadScene(eb.EmptyScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(eb.ContainmentScene);
            playerPrompts.CriticalAlert("Entity Escaped");
        }
    }

    public void SetLockdown(bool lockdown)
    {
        onLockdown = lockdown;
        foreach (Light l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        if (onLockdown) playerPrompts.CriticalAlert("LockDown Activated");
        if (onLockdown) RedAlert.UnPause(); else RedAlert.Pause();
    }

    public void InvertLockdown()
    {
        SetLockdown(!onLockdown);
    }
}
