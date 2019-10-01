using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{

    #region Level and Sets
    [Header("Level and Set")]
    [SerializeField] eSet m_curentSet = eSet.NONE;
    public eSet CurentSet { get => m_curentSet; set { SetSet(value); } }

    public enum eSet
    {
        NONE,
        MENU,
        GAME,
        CINAMATIC,
        DEBUG,
    }

    public void SetSet(eSet set)
    {
        m_curentSet = set;
        ChangeSet();
    }

    void ChangeSet()
    {
        switch (CurentSet)
        {
            case eSet.NONE:
                Cursor.visible = true;
                break;
            case eSet.MENU:
                Cursor.visible = true;
                break;
            case eSet.GAME:
                Cursor.visible = false;
                break;
            case eSet.CINAMATIC:
                Cursor.visible = false;
                break;
            case eSet.DEBUG:
                InitDebug();
                break;
            default:
                break;
        }

        if (CurentSet != eSet.DEBUG) RemoveDebug();
    }

    void UpdateSet()
    {
        switch (CurentSet)
        {
            case eSet.NONE:
                break;
            case eSet.MENU:
                break;
            case eSet.GAME:
                break;
            case eSet.CINAMATIC:
                break;
            case eSet.DEBUG:
                UpdateDebug();
                break;
            default:
                break;
        }
    }

    public void LoadLevel(string level, eSet set = eSet.GAME)
    {
        m_curentSet = set;
        LevelManager.Instance.LoadLevel(level);
        ChangeSet();
    }

    public void LoadLevel(int level, eSet set = eSet.GAME)
    {
        LoadLevel(SceneManager.GetSceneAt(level).name, set);
    }

    #endregion

    #region MonoBehavior

    private void Start()
    {
        SetSet(CurentSet);

        TimeLease.Start();
    }

    private void Update()
    {
        UpdateSet();

        TimeLease.Update();
    }

    #endregion

    #region Debug

    [SerializeField] Canvas m_debugCanvasPrefab = null;

    Canvas m_debugCanvas = null;
    TextMeshProUGUI m_debugTMPro = null;
    Dictionary<string, string> m_debugValues = new Dictionary<string, string>();

    float m_FPSTicker = 0.0f;

    void InitDebug()
    {
        m_debugValues.Clear();
        if (m_debugCanvas == null)
        {
            m_debugCanvas = Instantiate(m_debugCanvasPrefab, Vector3.zero, Quaternion.identity, this.transform);
            m_debugTMPro = m_debugCanvas.GetComponentInChildren<Transform>().GetComponentInChildren<TextMeshProUGUI>();
        }
        m_debugCanvas.enabled = true;

    }

    void UpdateDebug()
    {
        // FPS
        if (m_FPSTicker > 0.25f)
        {
            SetDebugValue("FPS", (Mathf.Round(1 / Time.deltaTime)).ToString());
            m_FPSTicker = 0;
        }
        m_FPSTicker += Time.deltaTime;

        // Debug Values
        string Values = "";
        foreach (KeyValuePair<string, string> pair in m_debugValues)
        {
            Values += pair.Key + ": " + pair.Value + "\n";
        }
        m_debugTMPro.text = Values;
    }

    void RemoveDebug()
    {
        if (m_debugCanvas != null) m_debugCanvas.enabled = false;
    }

    public void SetDebugValue(string key, string value)
    {
        if (m_debugValues.ContainsKey(key))
        {
            m_debugValues[key] = value;
        }
        else
        {
            m_debugValues.Add(key, value);
        }
    }

    public void RemoveDebugValue(string key)
    {
        if (m_debugValues.ContainsKey(key))
        {
            m_debugValues.Remove(key);
        }
    }

    #endregion

}