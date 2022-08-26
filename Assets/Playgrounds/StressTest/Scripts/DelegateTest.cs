using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playgrounds.StressTest.Scripts
{
    public class DelegateTest : MonoBehaviour
    {
        private List<Action> _action = new List<Action>(2*2*2*2*2*2*2*2*2*2*2*2*2*2*2);

        public void Test()
        {
            for (int i = 0; i < 10_000; i++)
            {
                _action.Add(() => Debug.Log(i));
            }
        }
    }
}