using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Updater : MonoBehaviour
    {
        private readonly List<IUpdateListener> _listeners = new List<IUpdateListener>();

        public void Update()
        {
            foreach (var listener in _listeners)
            {
                listener.Tick(Time.deltaTime);
            }
        }

        public void AddListener(IUpdateListener listener)
        {
            _listeners.Add(listener);
        }
        
        public void RemoveListener(IUpdateListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}