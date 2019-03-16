using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerUnityEvents2D : MonoBehaviour
    {
        [SerializeField] private UnityEvent _triggerEntered;
        [SerializeField] private UnityEvent _triggerExited;

        public UnityEvent TriggerEntered => _triggerEntered;
        public UnityEvent TriggerExited => _triggerExited;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _triggerEntered.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _triggerExited.Invoke();
        }


    }
}