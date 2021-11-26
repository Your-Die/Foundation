namespace Chinchillada
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    public abstract class EventListenerBase : ChinchilladaBehaviour
    {
        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        [FormerlySerializedAs("_event")] [SerializeField] 
        private ScriptedEvent @event = null;

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
        /// Called when the <see cref="ScriptedEventListener.response"/> happened.
        /// Invokes the <see cref="ScriptedEventListener"/>.
        /// </summary>
        protected abstract void OnEventHappened();

        [Button]
        private void SimulateEvent() => this.OnEventHappened();
    }
}