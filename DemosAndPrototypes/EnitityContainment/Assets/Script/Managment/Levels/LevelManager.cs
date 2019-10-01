using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] List<Transitions> m_levelTransitions = new List<Transitions>();
    GameObject m_transitionObject = null;
    LevelTransistion m_levelTransition = null;

    [System.Serializable]
    struct Transitions
    {
        [SerializeField] public string name;
        [SerializeField] public GameObject levelTransistion;
    }

    public void LoadLevel(string level)
    {
        m_transitionObject = null;
        foreach(Transitions transition in m_levelTransitions)
        {
            if(transition.name == level)
            {
                m_transitionObject = Instantiate(transition.levelTransistion);
                break;
            }
        }
        if(m_transitionObject == null) m_transitionObject = Instantiate(m_levelTransitions[0].levelTransistion);
        m_levelTransition = m_transitionObject.GetComponent<LevelTransistion>();
        Debug.Log(m_levelTransition.name);

        m_levelTransition.loadingCanvas.enabled = true;
        StartCoroutine(LoadAsynchronously(level));
        m_levelTransition.loadingCanvas.enabled = false;
        Destroy(m_levelTransition);

    }

    IEnumerator LoadAsynchronously(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading " + level + " : " + (progress * 100) + "%");

            m_levelTransition.UpdateValue(progress);

            yield return null;
        }
    }

}


