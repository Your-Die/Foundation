using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Component that propagates collision events to C# events.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionEvents : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is entered.
        /// </summary>
        public event Action<Collision> CollisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is exited.
        /// </summary>
        public event Action<Collision> CollisionExited;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExited?.Invoke(collision);
        }
    }
}