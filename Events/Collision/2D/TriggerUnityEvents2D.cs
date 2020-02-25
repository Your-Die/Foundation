using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that propagates 2D trigger events to <see cref="UnityEvent"/>.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TriggerUnityEvents2D : MonoBehaviour
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _triggerEntered.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _triggerExited.Invoke();
        }


    }
}