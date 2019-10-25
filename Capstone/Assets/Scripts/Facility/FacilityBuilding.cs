using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
    [SerializeField] public Switch_Toggle LockDownSwitch;
    [SerializeField] GameObject UIPromptArrival;
    [SerializeField] GameObject UIPromptEscape;
    [SerializeField] bool LoadParabear = false;
    [SerializeField] bool LoadRainyDay = false;
    public bool onLockdown;

    Light[] lights;


    float InitialTimer = 10.0f;
    bool loaded = false;
    PlayerMove playerMove;


    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        lights = FindObjectsOfType<Light>();
        if (LoadParabear)
        {
             SceneManager.LoadScene("ParaBearContainmentRoom", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("EmptyParaBearContainmentRoom", LoadSceneMode.Additive);
        }
        if (LoadRainyDay)
        {
            SceneManager.LoadScene("RainyDayContainmentRoom", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("EmptyRainyDayContainmentRoom", LoadSceneMode.Additive);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (InitialTimer <= 0.0f && !loaded) // load first entity
        //{
        //  // SceneManager.UnloadSceneAsync("EmptyParaBearContainmentRoom");
        //  //  Instantiate(UIPromptArrival, playerMove.transform.position + playerMove.transform.forward, Quaternion.identity);
        //    loaded = true;
        //} else if (InitialTimer > 0.0f)
        //{
        //    InitialTimer -= Time.deltaTime;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityBasic eb = other.GetComponent<EntityBasic>();
        if (eb)
        {
            SceneManager.LoadScene(eb.EmptyScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(eb.ContainmentScene);
            Instantiate(UIPromptEscape, playerMove.transform.position + playerMove.transform.forward, Quaternion.identity);

        }
    }

    public void SetLockdown(bool lockdown)
    {
        onLockdown = lockdown;
        foreach (Light l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }

    public void InvertLockdown()
    {
        onLockdown = !onLockdown;
        foreach (Light l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }
}
