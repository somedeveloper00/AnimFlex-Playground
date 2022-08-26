using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Playgrounds.Shared;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProfileSaver : MonoBehaviour
{
    public TweenMaker[] multiSpawners;
        
    public UIElement activeTweensElement;
    public UIElement frameTimeElement;

    public UIElement totalCreatedTweensElement;
    public UIElement totalFramesElement;
    public UIElement averageFrameTimeElement;
    public UIElement medianFrameTimeElement;
    public UIElement averageFpsElement;
    public UIElement medianFpsElement;

    [Header("Pause")] 
    public bool pauseOnSlowFrames;
    public float slowFrameThreshold;
    
    private List<double> milliseconds = new();
    private Stopwatch _stopwatch = new();
    private bool stoped = false;
    private bool firstFrame = true;
    
    public void Stop() => stoped = true;

    private void Awake()
    {
        _stopwatch.Start();
    }

    private void Update()
    {
        // frame check end
        var value = _stopwatch.Elapsed.TotalMilliseconds;
        if (!firstFrame)
        {
            if(stoped) return;
            
            milliseconds.Add(value);
            frameTimeElement.Value = value.ToString();
            activeTweensElement.Value = multiSpawners.Sum(ms => ms.totalActive).ToString();

#if UNITY_EDITOR
            if (pauseOnSlowFrames && value >= slowFrameThreshold)
                EditorApplication.isPaused = true;
#endif
        }
        firstFrame = false;
            
        // frame check start
        _stopwatch.Restart();
    }

    public void ShowResults()
    {
        var sum = milliseconds.Sum();
        var average = sum / milliseconds.Count;
        var median = milliseconds.OrderBy(val => val).ToList()[milliseconds.Count / 2];
            
        totalCreatedTweensElement.Value = multiSpawners.Sum(ms=>ms.totalCreated).ToString();
        totalFramesElement.Value = milliseconds.Count.ToString();
        averageFrameTimeElement.Value = average.ToString();
        averageFpsElement.Value = (1000 / average).ToString();
        medianFrameTimeElement.Value = median.ToString();
        medianFpsElement.Value = (1000 / median).ToString();
    }

    public void ShowPlot()
    {
        Debug.Log(
            $"finished profiling elapsed Updates. captured {milliseconds.Count} frames.");
        var message = string.Join(", ", milliseconds);

        // check if python3 is installed
        string pythonCodePath = "Plots/plotter.py";
        var start = new ProcessStartInfo();
        start.FileName = @"py.exe";
        start.Arguments = $"{pythonCodePath} \"{message}\"";
        start.UseShellExecute = false;
        start.RedirectStandardError = true;
        start.CreateNoWindow = true;
        
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Debug.Log(result);
            }
        }
    }
}