using System;
using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that propagates 2D collision events to C# events.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is entered.
        /// </summary>
        public event Action<Collision2D> CollisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is exited.
        /// </summary>
        public event Action<Collision2D> CollisionExited;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExited?.Invoke(collision);
        }
    }
}