using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    /// <summary>
    /// Component that propagates collision events to <see cref="UnityEvent"/>.
    /// </summary>
    public class CollisionUnityEvents : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is entered.
        /// </summary>
        [SerializeField] private UnityEvent collisionEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is exited.
        /// </summary>
        [SerializeField] private UnityEvent collisionExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is entered.
        /// </summary>
        public UnityEvent CollisionEntered => this.collisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is exited.
        /// </summary>
        public UnityEvent CollisionExited => this.collisionExited;

        private void OnCollisionEnter(Collision _)
        {
            this.collisionEntered.Invoke();
        }

        private void OnCollisionExit(Collision _)
        {
            this.collisionExited.Invoke();
        }
    }
}
