using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class ProfileSaver : MonoBehaviour
    {
        public float totalCapturingSeconds = 30;

        private List<double> milliseconds = new();
        private Stopwatch _stopwatch = new();

        private float startTime;
        private float lastLog = 0;

        private void Start()
        {
            startTime = Time.unscaledTime;
        }

        private void Update()
        {
            if (_stopwatch.IsRunning)
            {
                milliseconds.Add(_stopwatch.Elapsed.TotalMilliseconds);
            }
            
            if (Time.unscaledTime - startTime > totalCapturingSeconds)
            {
                Destroy(gameObject);
                EditorApplication.isPaused = true;
                return;
            }
            
            // log elapsed time
            if (Time.unscaledTime - lastLog > 5)
            {
                Debug.Log($"time left (seconds): {startTime + totalCapturingSeconds - Time.unscaledTime}");
                EditorApplication.Beep();
                lastLog = Time.unscaledTime;
            }

            _stopwatch.Restart();
        }

        private void OnDisable()
        {
            Debug.Log(
                $"finished profiling elapsed Updates. captured {milliseconds.Count} frames. " +
                $"fastest frame: {milliseconds.Min()}. " +
                $"slowest frame: {milliseconds.Max()}");
            var message = string.Join(", ", milliseconds);
            Debug.Log(message);
            var path = "Plots/data.dat";
            File.WriteAllText(path, message);
            Debug.Log($"data saved to {path}");
        }
    }
}