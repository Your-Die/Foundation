using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that propagates 2D collision events to <see cref="UnityEvent"/>.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class CollisionUnityEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is entered.
        /// </summary>
        [SerializeField] private UnityEvent _collisionEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is exited.
        /// </summary>
        [SerializeField] private UnityEvent _collisionExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is entered.
        /// </summary>
        public UnityEvent CollisionEntered => _collisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is exited.
        /// </summary>
        public UnityEvent CollisionExited => _collisionExited;

        private void OnCollisionExit2D(Collision2D collision)
        {
            _collisionEntered.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _collisionExited.Invoke();
        }
    }
}