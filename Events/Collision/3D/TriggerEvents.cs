using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEvents : MonoBehaviour
    {
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExited?.Invoke(other);
        }
    }
}