using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    /// <summary>
    /// Generic version of <see cref="ScriptedEvent"/> that allows a parameter of {T} to be passed along.
    /// </summary>
    /// <typeparam name="T">The type of parameter we want to pass along with the event.</typeparam>
    public class ScriptedEventBase<T> : ScriptableObject, IInvokableEvent<T>
    {
        private readonly TypedEvent<T> @event = new TypedEvent<T>();
        public void Invoke(T context) => this.@event.Invoke(context);

        public void Subscribe(Action action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action action) => this.@event.Unsubscribe(action);

        public void Subscribe(Action<T> action) => this.@event.Subscribe(action);

        public void Unsubscribe(Action<T> action) => this.@event.Unsubscribe(action);
    }
}