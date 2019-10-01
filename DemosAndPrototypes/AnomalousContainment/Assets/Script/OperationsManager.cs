using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperationsManager : MonoBehaviour
{
    void Start()
    {
        
    }

     void Update()
    {
    }


    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
