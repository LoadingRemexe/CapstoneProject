using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacilityBuilding : MonoBehaviour
{
    [SerializeField] public Transform Exit;
    [SerializeField] public Switch_Toggle LockDownSwitch;
    [SerializeField] GameObject UIPromptArrival;
    [SerializeField] GameObject UIPromptEscape;
    public bool onLockdown;

    FacilityLight[] lights;

    PlayerMove playerMove;


    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<FacilityLight>();
        SceneManager.LoadScene("ParaBearContainmentRoom", LoadSceneMode.Additive);
        playerMove = FindObjectOfType<PlayerMove>();
        Instantiate(UIPromptArrival, playerMove.transform.position + playerMove.transform.forward, Quaternion.identity);

    }

    // Update is called once per frame
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
            Instantiate(UIPromptEscape, playerMove.transform.position + playerMove.transform.forward, Quaternion.identity);

        }
    }

    public void SetLockdown(bool lockdown)
    {
        onLockdown = lockdown;
        foreach (FacilityLight l in lights)
        {
            l.lightFixture.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }

    public void InvertLockdown()
    {
        onLockdown = !onLockdown;
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light l in lights)
        {
            l.color = (onLockdown) ? Color.red : Color.white;
        }
        Debug.Log("Lockdown set to " + onLockdown);
        LockDownSwitch.SetSwitch(onLockdown);
    }
}
