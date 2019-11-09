using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_SwitchScene : MonoBehaviour
{
    [SerializeField] string SceneName = "";
    public void SwitchScene()
    {
        //SceneManager.LoadScene(SceneName);
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneName));
    }
}
