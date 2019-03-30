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
        [SerializeField] private List<KeyboardEvent> _keyboardEvents = new List<KeyboardEvent>();

        /// <summary>
        /// Add the <paramref name="keyboardEvent"/> to this collection.
        /// </summary>
        public void Add(KeyboardEvent keyboardEvent)
        {
            _keyboardEvents.Add(keyboardEvent);
        }

        /// <summary>
        /// Create and add a new <see cref="KeyboardEvent"/> to this collection.
        /// </summary>
        /// <param name="key">The key we want to create events on.</param>
        /// <returns></returns>
        public KeyboardEvent Add(KeyCode key, UnityAction onKeyUp = null, UnityAction onKeyDown = null)
        {
            KeyboardEvent keyboardEvent = new KeyboardEvent(key, onKeyUp, onKeyDown);
            Add(keyboardEvent);

            return keyboardEvent;
        }

        private void Update()
        {
            // Update the events.
            foreach (KeyboardEvent keyboardEvent in _keyboardEvents)
                keyboardEvent.Update();
        }
    }
}