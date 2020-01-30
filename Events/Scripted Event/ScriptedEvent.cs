using System;
using UnityEngine;

namespace Chinchillada.Events
{
    /// <summary>
    /// Scriptable object that contains an event that can be raised.
    /// Meant to able to be shared across decoupled systems.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Event", fileName = "Event")]
    public class ScriptedEvent : ScriptableObject
    {
        /// <summary>
        /// The event.
        /// </summary>
        public event Action Happened;

        /// <summary>
        /// Raises the <see cref="Happened"/>.
        /// </summary>
        public void Raise() => this.Happened?.Invoke();
    }
}