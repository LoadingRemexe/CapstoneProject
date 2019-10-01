using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeLease
{
    struct Ticker
    {
        public string name;
        public float time;
        public float percent;
        public float target;
        public bool targetAchieved;
        public bool inUse;

        public void Init(string name, float target)
        {
            this.name = name;
            time = 0;
            percent = 0.0f;
            this.target = target;
            targetAchieved = false;
            inUse = true;
            if (TimeLease.m_debugLog) Debug.Log("TimeLease- " + name + " Initiated");
        }
        public void AddDelta(float deltaTime)
        {
            time += deltaTime;
            percent = Mathf.Clamp01(time / target);
            if (time >= target)
            {
                targetAchieved = true;
                if (TimeLease.m_debugLog) Debug.Log("TimeLease- " + name + " Target Achieved");
            }
        }
        public void ResetTicker()
        {
            time = 0;
            targetAchieved = false;
            inUse = true;
            if (TimeLease.m_debugLog) Debug.Log("TimeLease- " + name + " Reset");
        }
        public void Destory()
        {
            if (TimeLease.m_debugLog) Debug.Log("TimeLease- " + name + " Destoryed");
            name = "";
            time = 0.0f;
            target = int.MaxValue;
            targetAchieved = false;
            inUse = false;
        }
    }

    static int m_TickerCount = 32;
    static bool m_resize = true;
    static bool m_debugLog = false;
    static bool m_debugValue = false;
    static Ticker[] tickers = new Ticker[1];

    #region MonoBehavior

    public static void Start()
    {
        tickers = new Ticker[m_TickerCount];
        foreach (Ticker ticker in tickers)
        {
            ticker.Destory();
        }
        if (m_debugValue) GameManager.Instance.SetDebugValue("Time Leases", AvalibleTimes().ToString());

    }
    public static void Update()
    {
        for (int i = 0; i < tickers.Length; i++)
        {
            if (tickers[i].inUse && !tickers[i].targetAchieved) tickers[i].AddDelta(Time.deltaTime);
        }
    }

    #endregion

    #region Add/Remove

    static public int NewTime(string name, float target, bool resetIfFound = true)
    {
        int search = FindTicker(name);
        if (search != -1)
        {
            if (resetIfFound) tickers[search].ResetTicker();
            return search;
        }

        for (int i = 0; i < tickers.Length; i++)
        {
            if (!tickers[i].inUse)
            {
                tickers[i].Init(name, target);
                if (m_debugValue) DebugValue();
                return i;
            }
        }

        if (m_resize)
        {
            DoubleLease();
            return NewTime(name, target, resetIfFound);
        }

        if (m_debugValue) DebugValue();
        return -1;
    }
    static public string NewTime(int index, float target, bool resetIfFound = true)
    {
        if (index > tickers.Length) throw new System.IndexOutOfRangeException();
        if (tickers[index].inUse)
        {
            if (resetIfFound) tickers[index].ResetTicker();
            return tickers[index].name;
        }

        string genaratedName = Random.value.ToString();
        tickers[index].Init(genaratedName, target);
        if (m_debugValue) DebugValue();
        return genaratedName;
    }
    static public void RemoveTime(string name)
    {
        RemoveTime(FindTicker(name));
    }
    static public void RemoveTime(int index)
    {
        tickers[index].Destory();
        if (m_resize) HalfLease();
        if (m_debugValue) DebugValue();
    }
    static public void RemoveTimes(string[] names)
    {
        foreach (string name in names)
        {
            tickers[FindTicker(name)].Destory();
        }
        if (m_resize) HalfLease();
        if (m_debugValue) DebugValue();
    }
    static public void RemoveTimes(int[] indexes)
    {
        foreach (int index in indexes)
        {
            tickers[index].Destory();
        }
        if (m_resize) HalfLease();
        if (m_debugValue) DebugValue();
    }
    static public void RemoveTimes(int[] indexes, string[] names)
    {
        foreach (int index in indexes)
        {
            tickers[index].Destory();
        }
        foreach (string name in names)
        {
            tickers[FindTicker(name)].Destory();
        }
        if (m_resize) HalfLease();
        if (m_debugValue) DebugValue();
    }
    static public void Clear()
    {
        for (int i = 0; i < tickers.Length; i++)
        {
            tickers[i].Destory();
        }
        if (m_resize) HalfLease();
        if (m_debugValue) DebugValue();
    }
    static public void ResetLease(int tickerCount)
    {
        m_TickerCount = tickerCount;
        tickers = new Ticker[m_TickerCount];
        foreach (Ticker ticker in tickers)
        {
            ticker.Destory();
        }
        if (m_debugValue) DebugValue();
    }

    #endregion

    #region Check Time

    static public bool CheckTime(string name, float target, bool destory = false)
    {
        int index = FindTicker(name);

        if (index == -1)
        {
            index = NewTime(name, target);
            return true;
        }

        if (CheckTime(index))
        {
            if (destory)
            {
                tickers[FindTicker(name)].Destory();
                if (m_resize) HalfLease();
                if (m_debugValue) GameManager.Instance.SetDebugValue("Time Leases", AvalibleTimes().ToString());
            }
            return true;
        }
        return false;
    }
    static public bool CheckTime(string name, bool destory = true)
    {
        int index = FindTicker(name);
        if (CheckTime(index))
        {
            if (destory && index != -1) tickers[index].Destory();
            if (m_resize) HalfLease();
            if (m_debugValue) GameManager.Instance.SetDebugValue("Time Leases", AvalibleTimes().ToString());
            return true;
        }
        return false;
    }
    static public bool CheckTime(int index)
    {
        if (index == -1) return true;
        if (tickers[index].targetAchieved)
        {
            tickers[index].ResetTicker();
            return true;
        }
        return false;
    }

    #endregion

    #region Resize

    static public void DoubleLease()
    {
        m_TickerCount = tickers.Length * 2;
        Ticker[] newTicker = new Ticker[m_TickerCount];
        tickers.CopyTo(newTicker, 0);
        for(int i = (m_TickerCount / 2); i < m_TickerCount; i++)
        {
            newTicker[i].Destory();
        }
        tickers = newTicker;
        m_TickerCount = tickers.Length;
    }
    static public void HalfLease()
    {
        bool halfing = true;
        while (halfing)
        {
            int unAvalible = tickers.Length - AvalibleTimes();
            if (unAvalible < (Mathf.RoundToInt(tickers.Length * .5f)) && unAvalible > 1)
            {
                m_TickerCount = Mathf.RoundToInt(tickers.Length * .5f);
                Ticker[] newTicker = new Ticker[m_TickerCount];
                int incro = 0;
                foreach (Ticker ticker in tickers)
                {
                    if (ticker.inUse)
                    {
                        newTicker[incro] = ticker;
                        incro++;
                    }
                }
                tickers = newTicker;
            }
            else
            {
                halfing = false;
            }
        }
        m_TickerCount = tickers.Length;
    }

    #endregion

    #region Helper

    static public int ResetTicker(string name)
    {
        return ResetTicker(FindTicker(name));
    }
    static public int ResetTicker(int index)
    {
        if (index > tickers.Length) throw new System.IndexOutOfRangeException();
        if(tickers[index].inUse)
        {
            tickers[index].ResetTicker();
            return index;
        }
        return -1;
    }
    static public int FindTicker(string name)
    {
        for (int i = 0; i < tickers.Length; i++)
        {
            if (tickers[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
    static public float GetPercent(string name)
    {
        return GetPercent(FindTicker(name));
    }
    static public float GetPercent(int index)
    {
        return tickers[index].percent;
    }
    static private int AvalibleTimes()
    {
        int count = 0;
        for (int i = 0; i < m_TickerCount; i++)
        {
            if (!tickers[i].inUse) count++;
        }
        return count;
    }
    static private void DebugValue()
    {
        if (m_resize)
        {
            GameManager.Instance.SetDebugValue("Time InUse", (m_TickerCount - AvalibleTimes()).ToString());
        }
        else
        {
            GameManager.Instance.SetDebugValue("Time Avalible", AvalibleTimes().ToString());
        }
    }

    #endregion

}