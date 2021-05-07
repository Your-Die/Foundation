using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    /// <summary>
    /// Class that propagates key presses as <see cref="UnityEvent"/>.
    /// </summary>
    [Serializable]
    public class KeyboardEvent
    {
        /// <summary>
        /// The key we invoke events on.
        /// </summary>
        [SerializeField] private KeyCode key = KeyCode.Escape;

        [SerializeField] private Events events = new Events();
        
        /// <summary>
        /// The key we invoke events on.
        /// </summary>
        public KeyCode Key
        {
            get => this.key;
            set => this.key = value;
        }

        /// <summary>
        /// Event invoked when the <see cref="Key"/> is pressed.
        /// </summary>
        public UnityEvent KeyDown => this.events.keyDown;

        /// <summary>
        /// Event invoked when the <see cref="Key"/> is released.
        /// </summary>
        public UnityEvent KeyUp => this.events.keyUp;

        /// <summary>
        /// Constructs a new <see cref="KeyboardEvent"/>.
        /// </summary>
        /// <param name="key">The key we want to construct events around.</param>
        /// <param name="onKeyUp">Optional callback to add to the <see cref="KeyUp"/> event.</param>
        /// <param name="onKeyDown">Optional callback to add to the <see cref="KeyDown"/> event.</param>
        public KeyboardEvent(KeyCode key = KeyCode.Escape, UnityAction onKeyUp = null, UnityAction onKeyDown = null)
        {
            this.key = key;

            if (onKeyUp != null)
                this.events.keyUp.AddListener(onKeyUp);

            if (onKeyDown != null)
                this.events.keyDown.AddListener(onKeyDown);
        }

        /// <summary>
        /// Checks the state of the <see cref="Key"/> and invokes events if appropriate.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown(this.key))
                this.KeyDown.Invoke();

            else if (Input.GetKeyUp(this.key)) 
                this.KeyUp.Invoke();
        }

        [Serializable]
        private class Events
        {
            /// <summary>
            /// Event invoked when the <see cref="key"/> is pressed.
            /// </summary>
            public UnityEvent keyDown = new UnityEvent();

            /// <summary>
            /// Event invoked when the <see cref="key"/> is released.
            /// </summary>
            public UnityEvent keyUp = new UnityEvent();
        }
    }
}
