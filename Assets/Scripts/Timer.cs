using System;
using System.Diagnostics;
using UnityEngine;

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
    }
}
