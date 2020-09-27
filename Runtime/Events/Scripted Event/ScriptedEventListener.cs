using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Component that propagates <see cref="ScriptedEvent"/> to <see cref="UnityEvent"/>.
    /// </summary>
    public class ScriptedEventListener : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        [FormerlySerializedAs("_event")] [SerializeField] 
        private ScriptedEvent @event = null;

        /// <summary>
        /// The event invoked when the <see cref="Event"/> is raised.
        /// </summary>
        public UnityEvent response;

        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        public ScriptedEvent Event
        {
            get => this.@event;
            set => this.@event = value;
        }

        /// <summary>
        /// Subscribes to the <see cref="Event"/>.
        /// </summary>
        private void OnEnable() => this.Event.Happened += this.OnEventHappened;

        /// <summary>
        /// Unsubscribes from the <see cref="Event"/>.
        /// </summary>
        private void OnDisable() => this.Event.Happened -= this.OnEventHappened;

        /// <summary>
        /// Called when the <see cref="Event"/> happened.
        /// Invokes the <see cref="response"/>.
        /// </summary>
        private void OnEventHappened() => this.response?.Invoke();

        [Button]
        private void SimulateEvent() => this.OnEventHappened();
    }
}