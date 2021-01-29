using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] GameObject optionsMenu = default;
    [SerializeField] Toggle timeToggle = default;
    [SerializeField] Slider sfxVolumeSlider = default;
    [SerializeField] Slider musicVolumeSlider = default;

    SettingsHolder settings;

    private void Start()
    {
        settings = FindObjectOfType<SettingsHolder>();
        optionsMenu.SetActive(false);
        AddListeners();
        ResetParameters();
    }

    private void AddListeners()
    {
        timeToggle.onValueChanged.AddListener(SetTimer);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void ResetParameters()
    {
        timeToggle.isOn = settings.isTimerEnabled();
        sfxVolumeSlider.value = settings.GetSFXVolume();
        musicVolumeSlider.value = settings.GetMusicVolume();
    }

    public void GoToOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetTimer(bool value)
    {
        settings.SetTimerEnabled(value);
    }

    public void SetSFXVolume(float value)
    {
        settings.SetSFXVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        settings.SetMusicVolume(value);
    }
}
