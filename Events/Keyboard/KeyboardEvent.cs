using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
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
        [SerializeField] private KeyCode _key = KeyCode.Escape;

        /// <summary>
        /// Event invoked when the <see cref="_key"/> is pressed.
        /// </summary>
        [SerializeField] private UnityEvent _keyDown = new UnityEvent();

        /// <summary>
        /// Event invoked when the <see cref="_key"/> is released.
        /// </summary>
        [SerializeField] private UnityEvent _keyUp = new UnityEvent();

        /// <summary>
        /// The key we invoke events on.
        /// </summary>
        public KeyCode Key
        {
            get => _key;
            set => _key = value;
        }

        /// <summary>
        /// Event invoked when the <see cref="Key"/> is pressed.
        /// </summary>
        public UnityEvent KeyDown => _keyDown;

        /// <summary>
        /// Event invoked when the <see cref="Key"/> is released.
        /// </summary>
        public UnityEvent KeyUp => _keyUp;

        /// <summary>
        /// Constructs a new <see cref="KeyboardEvent"/>.
        /// </summary>
        /// <param name="key">The key we want to construct events around.</param>
        /// <param name="onKeyUp">Optional callback to add to the <see cref="KeyUp"/> event.</param>
        /// <param name="onKeyDown">Optional callback to add to the <see cref="KeyDown"/> event.</param>
        public KeyboardEvent(KeyCode key = KeyCode.Escape, UnityAction onKeyUp = null, UnityAction onKeyDown = null)
        {
            _key = key;

            if (onKeyUp != null)
                _keyUp.AddListener(onKeyUp);

            if (onKeyDown != null)
                _keyDown.AddListener(onKeyDown);
        }

        /// <summary>
        /// Checks the state of the <see cref="Key"/> and invokes events if appropriate.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown(_key))
                KeyDown.Invoke();

            else if (Input.GetKeyUp(_key))
                KeyUp.Invoke();
        }
    }
}
