using UnityEngine;
using UnityEngine.Events;

namespace Mutiny.Foundation.Events
{
    public class SimpleEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent @event;

        protected UnityEvent Event => this.@event;
    }
}