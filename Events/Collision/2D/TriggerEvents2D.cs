using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Component that propagates 2D trigger events to C# events.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TriggerEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a trigger is entered.
        /// </summary>
        public event Action<Collider2D> TriggerEntered;

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
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