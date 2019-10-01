using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public static class SaveSystem
{
    #region Log

    private static string m_logLocation = "saveLog";
    private static List<string> m_dataLog = new List<string>();

    private static string m_extention = ".contain";

    /// <summary>
    /// Saves Save Log and returns if successful
    /// </summary>
    /// <returns></returns>
    public static bool SaveLog()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + m_logLocation + ".log";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, m_dataLog);
            stream.Close();
            Debug.Log(Application.persistentDataPath + "/" + m_logLocation + ".log Updated");
            return true;
        }
        catch
        {
            Debug.LogError("Failed to Save Log");
            return false;
        }
    }
    /// <summary>
    /// Loads Save Log and returns if successful
    /// </summary>
    /// <returns></returns>
    public static bool LoadLog()
    {
        string path = Application.persistentDataPath + "/" + m_logLocation + ".log";
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                m_dataLog = (List<string>)formatter.Deserialize(stream);
                stream.Close();

                return true;
            }
            catch
            {
                Debug.LogError("Failed to Load Log");
                return false;
            }
        }
        else
        {
            Debug.Log("File not found at " + (Application.persistentDataPath + "/" + m_logLocation + ".log") + ", Creating new file...");
            SaveLog();
            return false;
        }
    }

    #endregion
    #region Data

    /// <summary>
    /// Saves Data to file
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="data">Data to save</param>
    /// <returns></returns>
    public static bool Save(string name, object data)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + name + m_extention;
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch
        {
            Debug.LogError("Failed to create save at " + Application.persistentDataPath + "/" + name + m_extention + ", Type of " + data.GetType());
            return false;
        }

        // Update Datalog
        if (!m_dataLog.Contains(name))
        {
            m_dataLog.Add(name);
            SaveLog();
        }

        return true;
    }

    /// <summary>
    /// Loads data from file
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    /// <param name="name">File name</param>
    /// <returns></returns>
    public static T Load<T>(string name)
    {
        string path = Application.persistentDataPath + "/" + name + m_extention;
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();

                return data;
            }
            catch
            {
                Debug.LogError("Failed To Load data, type of " + typeof(T));
                return default(T);
            }
        }
        else
        {
            Debug.Log("File not found at " + (Application.persistentDataPath + "/" + name + m_extention));
            return default(T);
        }
    }

    /// <summary>
    /// Destorys saved data file
    /// </summary>
    /// <param name="name">file name</param>
    /// <returns></returns>
    public static bool Destory(string name)
    {
        string path = Application.persistentDataPath + "/" + name + m_extention;
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                m_dataLog.Remove(name);
                SaveLog();
                return true;
            }
            catch
            {
                Debug.LogError("Failed to Delete file at " + Application.persistentDataPath + "/" + name + m_extention);
                return false;
            }
        }
        Debug.Log("File not found at " + (Application.persistentDataPath + "/" + name + m_extention));
        return false;
    }

    /// <summary>
    /// Destorys all saved files
    /// </summary>
    public static void ResetAll()
    {
        foreach(string entry in m_dataLog)
        {
            Destory(entry);
        }
    }

    #endregion
}
