using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{    /// <summary>
    /// Component that propagates trigger events to <see cref="UnityEvent"/>.
    /// </summary>
    public class TriggerUnityEvents : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a trigger is entered.
        /// </summary>
        [SerializeField] private UnityEvent triggerEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
        [SerializeField] private UnityEvent triggerExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a trigger is entered.
        /// </summary>
        public UnityEvent TriggerEntered => this.triggerEntered;

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
        public UnityEvent TriggerExited => this.triggerExited;

        private void OnTriggerEnter(Collider other)
        {
            this.triggerEntered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            this.triggerExited.Invoke();
        }
    }
}
