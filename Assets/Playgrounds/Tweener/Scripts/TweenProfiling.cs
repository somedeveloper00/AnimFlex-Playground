using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TweenProfiling : MonoBehaviour
{
    public int tweenCount = 10_000;
    public float duration = 5;
    public AnimFlex.Tweener.Ease ease;
    public Ease dotweenEase;
    
    void ProfileAnimFlex()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        Debug.Log($"animflex profile started: count: {tweenCount}, ease: {ease}");
        for (int i = 0; i < tweenCount; i++)
        {
            float value = 0;
            AnimFlex.Tweener.Tweener.Generate(
                getter: () => value,
                setter: (v) => value = v,
                endValue: 10,
                ease: ease,
                duration: duration,
                delay: 0);
        }
        stopwatch.Stop();
        Debug.Log($"start took {stopwatch.ElapsedMilliseconds:n} ms");

    }

    void ProfileDOTween()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        Debug.Log($"dotween profile started: count: {tweenCount}, ease: {ease}");
        for (int i = 0; i < tweenCount; i++)
        {
            float value = 0;
            DOTween.To(
                getter: () => value,
                setter: (v) => value = v,
                endValue: 10,
                duration: duration).SetEase(dotweenEase);
        }
        stopwatch.Stop();
        Debug.Log($"start took {stopwatch.ElapsedMilliseconds:n} ms");
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TweenProfiling))]
    private class edior : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Profile AnimFlex"))
            {
                (target as TweenProfiling).ProfileAnimFlex();
            }
            if (GUILayout.Button("Profile Dotween"))
            {
                (target as TweenProfiling).ProfileDOTween();
            }
        }
    }
#endif
}
