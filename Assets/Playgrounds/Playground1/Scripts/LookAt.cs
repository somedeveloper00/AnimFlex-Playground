using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Playgrounds.Playground1.Scripts
{
    public class LookAt : MonoBehaviour
    {
        public Transform[] target;

        private void Update()
        {
            transform.LookAt(GetCenter(target.Select(t => t.position).ToArray()));
        }

        public Vector3 GetCenter(Vector3[] vec3)
        {
            var sum = Vector3.zero;
            foreach (var v in vec3) sum += v;
            return sum / vec3.Length;
        }
    }
}