using System;
using System.Collections;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

public class TweenMaker : MonoBehaviour
{
    public int count;
    public float createPerFrame;
    public float duration;
    public UnityEvent onComplete;

    [NonSerialized] public long totalCreated = 0;
    [NonSerialized] public int totalActive = 0;
    
    private void Start() => Spawn();

    private void Spawn()
    {
        StartCoroutine(spawnCoroutine());
            
            
        IEnumerator spawnCoroutine()
        {
            int createdThisFrame = 0;
            for (int i = 0; i < count; i++)
            {
                totalCreated++;
                totalActive++;

                Action onTweenComplete = () => totalActive--;
                if (i == count - 1)
                    onTweenComplete += onComplete.Invoke;

                Profiler.BeginSample("Creating Tween");
                Vector3 pos = Vector3.left;
                var tween = Tweener.Generate(
                    getter: () => pos,
                    setter: (value) => pos = value,
                    duration: duration,
                    endValue: Vector3.right);
                tween.onComplete += onTweenComplete;
                Profiler.EndSample();

                if (++createdThisFrame >= createPerFrame)
                {
                    createdThisFrame = 0;
                    yield return null;
                }
            }
        }
    }
}