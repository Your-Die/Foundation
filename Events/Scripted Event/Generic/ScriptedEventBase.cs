using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Generic version of <see cref="ScriptedEvent"/> that allows a parameter of {T} to be passed along.
    /// </summary>
    /// <typeparam name="T">The type of parameter we want to pass along with the event.</typeparam>
    public class ScriptedEventBase<T> : ScriptableObject, IEvent<T>
    {
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
    }
}
