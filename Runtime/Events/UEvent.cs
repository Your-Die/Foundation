using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    [Serializable]
    public class UEvent : IInvokableEvent
    {
        [SerializeField] private UnityEvent @event;
        
        public void Subscribe(Action action) => this.@event.AddListener(action.Invoke);

        public void Unsubscribe(Action action) => this.@event.RemoveListener(action.Invoke);

        public void Invoke() => this.@event.Invoke();
    }
}