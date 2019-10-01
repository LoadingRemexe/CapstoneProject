using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] float m_autoSaveTime = 300.0f;
    private string m_timeLeaseName = "SaveManager";

    private List<ISavable> m_autoSaveObjects = new List<ISavable>();

    void Start()
    {
        TimeLease.NewTime(m_timeLeaseName, m_autoSaveTime);
    }

    void Update()
    {
        if(TimeLease.CheckTime(m_timeLeaseName, m_autoSaveTime))
        {
            foreach(ISavable save in m_autoSaveObjects)
            {
                save.Save();
            }
        }
    }

    public void SaveAll()
    {
        foreach (ISavable save in m_autoSaveObjects)
        {
            save.Save();
        }
    }

    public void AddSave(ISavable save)
    {
        m_autoSaveObjects.Add(save);
    }

    public void RemoveSave(ISavable save, bool SaveBeforeRemove = true)
    {
        if (SaveBeforeRemove) save.Save();
        m_autoSaveObjects.Remove(save);
    }
}
