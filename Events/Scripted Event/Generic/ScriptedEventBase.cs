using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Generic version of <see cref="ScriptedEvent"/> that allows a parameter of {T} to be passed along.
    /// </summary>
    /// <typeparam name="T">The type of parameter we want to pass along with the event.</typeparam>
    public class ScriptedEventBase<T> : ScriptableObject, IInvokableEvent<T>
    {
        private readonly Dictionary<Action, Listener> listeners = new Dictionary<Action, Listener>();

        public event Action<T> Happened;

        /// <summary>
        /// Raises this <see cref="ScriptedEvent"/> and passes along the <paramref name="context"/>.
        /// </summary>
        public void Invoke(T context)
        {
            this.Happened?.Invoke(context);
        }

        public void Subscribe(Action<T> action)
        {
            this.Happened += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            this.Happened -= action;
        }

        void IEvent.Subscribe(Action action)
        {
            var listener = new Listener(action);
            
            this.listeners.Add(action, listener);
            this.Happened += listener.Invoke;
        }

        void IEvent.Unsubscribe(Action action)
        {
            if (!this.listeners.TryGetValue(action, out var listener))
                return;
                
            this.Happened -= listener.Invoke;
            this.listeners.Remove(action);
        }

        private class Listener
        {
            private readonly Action action;

            public Listener(Action action)
            {
                this.action = action;
            }

            public void Invoke(T _)
            {
                this.action.Invoke();
            }
        }
    }
}