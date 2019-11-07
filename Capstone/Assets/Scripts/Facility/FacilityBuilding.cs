using UnityEngine;
using UnityEngine.SceneManagement;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
    [SerializeField] public Switch_Toggle LockDownSwitch;
    [SerializeField] AudioSource RedAlert;

    bool[] containmentRoomsLoaded = new bool[5];
    public bool onLockdown;

    Light[] lights;

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
        updateEntityStatus(1, false);
        updateEntityStatus(2, false);
        updateEntityStatus(3, false);
        updateEntityStatus(4, false);
        updateEntityStatus(5, false);
        SceneManager.LoadScene("RainyDayContainmentRoom", LoadSceneMode.Additive);
        SceneManager.LoadScene("ParaBearContainmentRoom", LoadSceneMode.Additive);

    }

    void Update()
    {

    }

    public void updateEntityStatus(int roomnum, bool Loaded)
    {
        containmentRoomsLoaded[roomnum - 1] = Loaded;
        string LoadInPrompts = "";
        for (int i = 1; i < 6; i++)
        {
            LoadInPrompts += "Observation Room " + i.ToString() + ": \n";
            if (containmentRoomsLoaded[i - 1])
            {
                LoadInPrompts += "Entity Arrived\n";
            }
            else
            {
                LoadInPrompts += "Vacant \n";
            }
        }
        playerPrompts.UpdateText1(LoadInPrompts);
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityBasic eb = other.GetComponent<EntityBasic>();
        if (eb)
        {
            SceneManager.UnloadSceneAsync(eb.ContainmentScene);
            SceneManager.LoadScene(eb.ContainmentScene, LoadSceneMode.Additive);
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
