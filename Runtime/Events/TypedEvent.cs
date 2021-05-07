using System;
using System.Collections.Generic;

namespace Chinchillada
{
    public class TypedEvent<T> : IInvokableEvent<T>
    {
        private readonly Dictionary<Action, TypedListener<T>> listeners = new Dictionary<Action, TypedListener<T>>();

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

        public void Subscribe(Action action)
        {
            var listener = new TypedListener<T>(action);

            this.listeners.Add(action, listener);
            this.Happened += listener.Invoke;
        }

        public void Unsubscribe(Action action)
        {
            if (!this.listeners.TryGetValue(action, out var listener))
                return;

            this.Happened -= listener.Invoke;
            this.listeners.Remove(action);
        }
    }
}