namespace Chinchillada.Behavior
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class EventAction : IAction
    {
        [SerializeField] private UnityEvent @event = new();

        public void Trigger() => this.@event.Invoke();
    }
}