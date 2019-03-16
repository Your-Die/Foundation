using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [Serializable]
    public class KeyboardEvent
    {
        [SerializeField] private KeyCode _key;

        [SerializeField] private UnityEvent _keyDown;
        [SerializeField] private UnityEvent _keyUp;

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
