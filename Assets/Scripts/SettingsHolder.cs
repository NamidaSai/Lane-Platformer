using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    private TimeSpan[] levelTimes = new TimeSpan[5];
    [SerializeField] private bool timerEnabled = true;

    public static SettingsHolder instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetTimeForLevel(int levelIndex, TimeSpan timeSaved)
    {
        levelTimes[levelIndex] = timeSaved;
    }

    public TimeSpan GetTimeForLevel(int levelIndex)
    {
        return levelTimes[levelIndex];
    }

    public bool isTimerEnabled()
    {
        return timerEnabled;
    }

    public void SetTimerEnabled(bool value)
    {
        timerEnabled = value;
    }
}
