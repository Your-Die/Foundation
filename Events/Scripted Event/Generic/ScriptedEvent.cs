using System;
using UnityEngine;

namespace Chinchillada.Utilities
{
    public class ScriptedEvent<T> : ScriptableObject
    {
        public Action<T> Happened;

        public void Raise(T context)
        {
            Happened?.Invoke(context);
        }
    }
}
