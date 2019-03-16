using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Utilities
{
    public class KeyboardEvents : MonoBehaviour
    {
        [SerializeField] private List<KeyboardEvent> _keyboardEvents = new List<KeyboardEvent>();

        private void Update()
        {
            foreach (var keyboardEvent in _keyboardEvents)
                keyboardEvent.Update();
        }
    }
}