using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Component that propagates trigger events to C# events.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerEvents : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a trigger is entered.
        /// </summary>
        public event Action<Collider> TriggerEntered;

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
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