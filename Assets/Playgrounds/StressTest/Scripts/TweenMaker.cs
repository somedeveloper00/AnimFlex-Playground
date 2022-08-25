using System;
using System.Collections;
using AnimFlex.Tweener;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Profiling;
using Tweener = AnimFlex.Tweener.Tweener;

public class TweenMaker : MonoBehaviour
{
    public bool useDotween = false;
    
    public int count;
    public float createPerFrame;
    public float duration;
        
    [NonSerialized]
    public bool isFinished = false;


        
    [NonSerialized] public int totalCreated = 0;
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

                Action onComplete = () => totalActive--;
                if (i == count - 1)
                    onComplete += () => isFinished = true;
                
                if (useDotween)
                {
                    Vector3 pos = Vector3.left;
                    var tween = DOTween.To(
                        getter: () => pos,
                        setter: (value) => pos = value,
                        duration: duration,
                        endValue: Vector3.right);
                    tween.onComplete += () => onComplete();
                }
                else
                {
                    Profiler.BeginSample("Creating Tween");
                    Vector3 pos = Vector3.left;
                    var tween = Tweener.Generate(
                        getter: () => pos,
                        setter: (value) => pos = value,
                        duration: duration,
                        endValue: Vector3.right);
                    tween.onComplete += onComplete;
                    Profiler.EndSample();
                }

                if (++createdThisFrame >= createPerFrame)
                {
                    createdThisFrame = 0;
                    yield return null;
                }
            }
        }
    }
}