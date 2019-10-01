using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveTransform : MonoBehaviour, ISavable
{
    [SerializeField] bool m_resetOnLoad = false;
    [Tooltip("Leave blank to autofill")]
    [SerializeField] string m_saveName;
    void Start()
    {
        SaveManager.Instance.AddSave(this);
        m_saveName = (m_saveName == null || m_saveName == "") ? gameObject.name + gameObject.GetInstanceID() : m_saveName;
        if (m_resetOnLoad) SaveSystem.Destory(m_saveName);
        Load();
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void OnDestroy()
    {
        SaveManager.Instance.RemoveSave(this);
    }

    public void Load()
    {
        List<object> loadData = SaveSystem.Load<List<object>>(m_saveName);
        if (loadData != null) SaveSystemSerialization.DeSerilizeTransform(loadData[0], transform);
    }

    public void Save()
    {
        List<object> saveData = new List<object>();
        saveData.Add(SaveSystemSerialization.SerilizeTransform(transform));
        SaveSystem.Save(m_saveName, saveData);
    }
}
