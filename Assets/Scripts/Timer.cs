using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Stopwatch stopwatch;
    public TimeSpan timeElapsed { get; private set; }

    private void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    private void Update()
    {
        timeElapsed = stopwatch.Elapsed;
    }

    public void StopTimer()
    {
        stopwatch.Stop();
        FindObjectOfType<SettingsHolder>().SetTimeForLevel(SceneManager.GetActiveScene().buildIndex - 2, timeElapsed);
    }
}
