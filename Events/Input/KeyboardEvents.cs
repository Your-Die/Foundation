using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Component that contains one or more instances of <see cref="KeyboardEvent"/>,
    /// and extends them to the inspector.
    /// </summary>
    public class KeyboardEvents : MonoBehaviour
    {
        /// <summary>
        /// The events.
        /// </summary>
        [SerializeField] private List<KeyboardEvent> events = new List<KeyboardEvent>();

        /// <summary>
        /// Add the <paramref name="event"/> to this collection.
        /// </summary>
        public void Add(KeyboardEvent @event)
        {
            this.events.Add(@event);
        }

        /// <summary>
        /// Create and add a new <see cref="KeyboardEvent"/> to this collection.
        /// </summary>
        /// <param name="key">The key we want to create events on.</param>
        /// <returns></returns>
        public KeyboardEvent Add(KeyCode key, UnityAction onKeyUp = null, UnityAction onKeyDown = null)
        {
            var @event = new KeyboardEvent(key, onKeyUp, onKeyDown);
            this.Add(@event);

            return @event;
        }

        private void Update()
        {
            // Update the events.
            foreach (var keyboardEvent in this.events)
                keyboardEvent.Update();
        }
    }
}