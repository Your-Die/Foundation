using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Foundation
{
    public class ObjectEvent : ChinchilladaBehaviour, IInvokableEvent
    {
        [SerializeField] private UnityEvent @event;
        
        public void Subscribe(Action action) => this.@event.AddListener(action.Invoke);

        public void Unsubscribe(Action action) => this.@event.RemoveListener(action.Invoke);

        public void Invoke() => this.@event.Invoke();
    }
    
    public class ObjectEvent<T> : ChinchilladaBehaviour, IInvokableEvent<T>
    {
        private readonly TypedEvent<T> @event = new TypedEvent<T>();
        public void Invoke(T context) => this.@event.Invoke(context);

        public void Subscribe(Action action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action action) => this.@event.Unsubscribe(action);

        public void Subscribe(Action<T> action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action<T> action) => this.@event.Unsubscribe(action);
    }
}