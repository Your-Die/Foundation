using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    public abstract class ScriptedEventListenerBase<T> : ChinchilladaBehaviour
    {
        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        [SerializeField] private ScriptedEventBase<T> @event = null;

        /// <summary>
        /// The event invoked when the <see cref="Event"/> is raised.
        /// </summary>
        public UnityEvent Response;

        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        public ScriptedEventBase<T> Event
        {
            get => this.@event;
            set => this.@event = value;
        }

    }
}