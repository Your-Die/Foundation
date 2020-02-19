using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Events
{
    public abstract class ScriptedEventListener<T> : ChinchilladaBehaviour
    {
        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        [SerializeField] private ScriptedEvent<T> @event = null;

        /// <summary>
        /// The event invoked when the <see cref="Event"/> is raised.
        /// </summary>
        public UnityEvent Response;

        /// <summary>
        /// The <see cref="ScriptedEvent"/> we listen to.
        /// </summary>
        public ScriptedEvent<T> Event
        {
            get => this.@event;
            set => this.@event = value;
        }

    }
}