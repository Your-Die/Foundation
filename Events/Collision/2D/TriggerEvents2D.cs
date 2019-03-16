using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerEvents2D : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;
        public event Action<Collider2D> TriggerExited;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerExited?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerEntered?.Invoke(collision);
        }
    }
}