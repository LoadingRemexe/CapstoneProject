using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_SwitchScene : MonoBehaviour
{
    [SerializeField] string SceneName = "";
    public void SceneButtonClicked()
    {
        SceneManager.LoadScene(SceneName);
      
    }
}
