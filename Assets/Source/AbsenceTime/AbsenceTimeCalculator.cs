using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsenceTimeCalculator : MonoBehaviour
{
    private readonly string _lastExitTimeKey = "LastExitTime";

    private void Start()
    {
        
    }

    public void SaveExitTime()
    {
        PlayerPrefs.SetString(_lastExitTimeKey, DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
    }

    public int GetAbsenceTimeInSeconds()
    {
        if (PlayerPrefs.HasKey(_lastExitTimeKey))
        {
            string lastExitTimeStr = PlayerPrefs.GetString(_lastExitTimeKey);
            if (DateTime.TryParse(lastExitTimeStr, out DateTime lastExitTime))
            {
                TimeSpan timeAway = DateTime.UtcNow - lastExitTime;

                return (int)timeAway.TotalSeconds;
            }
        }
        return 0;
    }

    private void OnApplicationQuit()
    {
        SaveExitTime();
    }
}
