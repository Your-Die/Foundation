using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
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
        [SerializeField] private UnityEvent collisionEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is exited.
        /// </summary>
        [SerializeField] private UnityEvent collisionExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is entered.
        /// </summary>
        public UnityEvent CollisionEntered => this.collisionEntered;

        /// <summary>
        /// Event invoked when a <see cref="Collision2D"/> is exited.
        /// </summary>
        public UnityEvent CollisionExited => this.collisionExited;

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.collisionEntered.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.collisionExited.Invoke();
        }
    }
}