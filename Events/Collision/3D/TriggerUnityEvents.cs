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
        [SerializeField] private UnityEvent _triggerEntered = new UnityEvent();

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
        [SerializeField] private UnityEvent _triggerExited = new UnityEvent();

        /// <summary>
        /// Event invoked when a trigger is entered.
        /// </summary>
        public UnityEvent TriggerEntered => _triggerEntered;

        /// <summary>
        /// Event invoked when a trigger is exited.
        /// </summary>
        public UnityEvent TriggerExited => _triggerExited;

        private void OnTriggerEnter(Collider other)
        {
            _triggerEntered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _triggerExited.Invoke();
        }
    }
}
