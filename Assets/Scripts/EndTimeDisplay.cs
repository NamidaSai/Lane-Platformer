using UnityEngine;
using System;
using TMPro;

public class EndTimeDisplay : MonoBehaviour
{
    [SerializeField] int levelIndex = 0;

    private void Start()
    {
        TextMeshProUGUI displayText = GetComponent<TextMeshProUGUI>();

        TimeSpan ts = FindObjectOfType<SettingsHolder>().GetTimeForLevel(levelIndex - 1);
        string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
            ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        displayText.text = elapsedTime;
    }


}