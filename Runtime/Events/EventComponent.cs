using System;
using UnityEngine;

namespace Chinchillada
{
    public class EventComponent : AutoRefBehaviour, IEvent
    {
        [SerializeReference] private IEvent @event;
        
        public void Subscribe(Action action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action action) => this.@event.Unsubscribe(action);
    }
    
    public class ObjectEvent<T> : AutoRefBehaviour, IInvokableEvent<T>
    {
        private readonly TypedEvent<T> @event = new TypedEvent<T>();
        public void Invoke(T context) => this.@event.Invoke(context);

        public void Subscribe(Action action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action action) => this.@event.Unsubscribe(action);

        public void Subscribe(Action<T> action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action<T> action) => this.@event.Unsubscribe(action);
    }
}