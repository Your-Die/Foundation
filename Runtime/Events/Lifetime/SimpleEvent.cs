using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Events
{
    public class SimpleEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent @event;

        protected UnityEvent Event => this.@event;

        [Button]
        protected void InvokeEvent() => this.Event.Invoke();
    }
}