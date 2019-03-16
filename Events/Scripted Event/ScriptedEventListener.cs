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
            _event.Raised += OnEventRaised;
        }

        private void OnDisable()
        {
            _event.Raised -= OnEventRaised;
        }

        public void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}