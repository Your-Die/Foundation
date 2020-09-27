using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Scriptable object that contains an event that can be raised.
    /// Meant to able to be shared across decoupled systems.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Event", fileName = "Event")]
    public class ScriptedEvent : ScriptableObject, IInvokableEvent
    {
        [SerializeField] private bool log = true;
        
        /// <summary>
        /// The event.
        /// </summary>
        public event Action Happened;

        /// <summary>
        /// Raises the <see cref="Happened"/>.
        /// </summary>
        [HideInEditorMode, Button]
        public void Invoke()
        {
            this.Happened?.Invoke();
            if (this.log)
            {
                Debug.Log($"{this.name} raised.");
            }
        }

        public void Subscribe(Action action)
        {
            this.Happened += action;
        }

        public void Unsubscribe(Action action)
        {
            this.Happened -= action;
        }

        public override string ToString() => this.name;
    }
}