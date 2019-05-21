using System;
using UnityEngine;

namespace Chinchillada.Events
{
    /// <summary>
    /// Generic version of <see cref="ScriptedEvent"/> that allows a parameter of {T} to be passed along.
    /// </summary>
    /// <typeparam name="T">The type of parameter we want to pass along with the event.</typeparam>
    public class ScriptedEvent<T> : ScriptableObject
    {
        public Action<T> Happened;

        /// <summary>
        /// Raises this <see cref="ScriptedEvent"/> and passes along the <paramref name="context"/>.
        /// </summary>
        public void Raise(T context)
        {
            Happened?.Invoke(context);
        }
    }
}
