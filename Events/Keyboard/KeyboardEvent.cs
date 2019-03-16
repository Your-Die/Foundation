using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [Serializable]
    public class KeyboardEvent
    {
        [SerializeField] private KeyCode _key = KeyCode.Escape;

        [SerializeField] private UnityEvent _keyDown = new UnityEvent();
        [SerializeField] private UnityEvent _keyUp = new UnityEvent();

        public UnityEvent KeyDown => _keyDown;
        public UnityEvent KeyUp => _keyUp;

        public void Update()
        {
            if (Input.GetKeyDown(_key))
                KeyDown.Invoke();

            else if (Input.GetKeyUp(_key))
                KeyUp.Invoke();
        }
    }
}
