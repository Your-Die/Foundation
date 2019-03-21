using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    public class ScriptedEventListener : MonoBehaviour
    {
        [SerializeField] private ScriptedEvent _event = null;

        public UnityEvent Response;

        private void OnEnable()
        {
            _event.Happened += OnEventHappened;
        }

        private void OnDisable()
        {
            _event.Happened -= OnEventHappened;
        }

        public void OnEventHappened()
        {
            Response?.Invoke();
        }
    }
}