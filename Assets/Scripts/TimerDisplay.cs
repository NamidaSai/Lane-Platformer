using System;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayText = default;

    private void Update()
    {
        TimeSpan ts = GetComponent<Timer>().timeElapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
            ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        displayText.text = elapsedTime;
    }
}