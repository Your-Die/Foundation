using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that propagates collision events to <see cref="UnityEvent"/>.
    /// </summary>
    public class CollisionUnityEvents : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is entered.
        /// </summary>
        [SerializeField] private UnityEvent _collisionEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is exited.
        /// </summary>
        [SerializeField] private UnityEvent _collisionExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is entered.
        /// </summary>
        public UnityEvent CollisionEntered => _collisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision"/> is exited.
        /// </summary>
        public UnityEvent CollisionExited => _collisionExited;

        private void OnCollisionEnter(Collision _)
        {
            _collisionEntered.Invoke();
        }

        private void OnCollisionExit(Collision _)
        {
            _collisionExited.Invoke();
        }
    }
}
