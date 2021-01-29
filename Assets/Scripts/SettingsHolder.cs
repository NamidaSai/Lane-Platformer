using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    private TimeSpan[] levelTimes = new TimeSpan[4];
    [SerializeField] private bool timerEnabled = true;

    private float sfxVolume = 1f;
    private float musicVolume = 1f;

    public static SettingsHolder instance;

    private MusicPlayer musicPlayer;
    private AudioManager audioManager;

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

    private void Start() 
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        audioManager = FindObjectOfType<AudioManager>();
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

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
    
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        audioManager.SetSFXVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicPlayer.SetMusicVolume(value);
    }

    public void SetTimerEnabled(bool value)
    {
        timerEnabled = value;
    }
}
