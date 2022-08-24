using System;
using System.Collections;
using System.Diagnostics;
using AnimFlex.Tweener;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DefaultNamespace
{
    public class MultiSpawner : MonoBehaviour
    {
        public GameObject spawningObj;
        public float distance;
        public int count;
        public float perSpawnerDelay;
        public float splitDelay;

        private static int totalSpawned = 0;
        
        private static int spawnerCount = 0;
        private int spawnerNumber;
        
        private void Start()
        {
            spawnerNumber = ++spawnerCount;
            Spawn();
        }

        private void Spawn()
        {
            StartCoroutine(spawnCoroutine());
            IEnumerator spawnCoroutine()
            {
                for (int i = 0; i < count; i++)
                {
                    var obj = Instantiate(spawningObj, GetPointPos(i),
                        transform.rotation, transform);
                    totalSpawned++;
                    yield return new WaitForSeconds(splitDelay + perSpawnerDelay * spawnerNumber);
                }

                Debug.Log($"spawned {totalSpawned} objects");
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < count; i++)
            {
                Gizmos.DrawSphere(GetPointPos(i), 0.5f);
            }
        }

        private Vector3 GetPointPos(int i)
        {
            return transform.position + transform.right * (i / 2f) * distance;
        }
    }
}