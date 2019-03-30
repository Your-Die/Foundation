using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that propagates <see cref="ScriptedEvent"/> to <see cref="UnityEvent"/>.
    /// </summary>
    public class ScriptedEventListener : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        [SerializeField] private ScriptedEvent _event = null;

        /// <summary>
        /// The event invoked when the <see cref="Event"/> is raised.
        /// </summary>
        public UnityEvent Response;

        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        public ScriptedEvent Event
        {
            get => _event;
            set => _event = value;
        }

        /// <summary>
        /// Subscribes to the <see cref="Event"/>.
        /// </summary>
        private void OnEnable()
        {
            Event.Happened += OnEventHappened;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="Event"/>.
        /// </summary>
        private void OnDisable()
        {
            Event.Happened -= OnEventHappened;
        }

        /// <summary>
        /// Called when the <see cref="Event"/> happened.
        /// Invokes the <see cref="Response"/>.
        /// </summary>
        public void OnEventHappened()
        {
            Response?.Invoke();
        }
    }
}