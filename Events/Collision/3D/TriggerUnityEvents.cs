using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    public class TriggerUnityEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent _triggerEntered = new UnityEvent();
        [SerializeField] private UnityEvent _triggerExited = new UnityEvent();

        public UnityEvent TriggerEntered => _triggerEntered;
        public UnityEvent TriggerExited => _triggerExited;

        private void OnTriggerEnter(Collider other)
        {
            _triggerEntered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _triggerExited.Invoke();
        }
    }
}
