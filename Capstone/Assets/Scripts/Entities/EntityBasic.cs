using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBasic : MonoBehaviour
{
    [SerializeField] public ContainmentRoom containmentRoom;
    [SerializeField] public string ContainmentScene;
    public float TimeInContainment = 0.0f;
    public string Statistics = "";

    private void Update()
    {
        TimeInContainment += Time.deltaTime;
        if (PlayerPrefs.GetFloat("LongestContainment") < TimeInContainment) PlayerPrefs.SetFloat("LongestContainment", TimeInContainment);
    }

}
