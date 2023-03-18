using System;
using Chinchillada.Behavior;
using UnityEngine;

namespace Chinchillada
{
    [Serializable]
    public class RaiseEventAction : IAction
    {
        [SerializeField] private ScriptedEvent @event;

        public void Trigger() => this.@event.Invoke();
    }
}
