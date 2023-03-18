using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Behavior
{
    public abstract class ActionComponentBase : AutoRefBehaviour, IAction
    {
        [Button]
        public abstract void Trigger();
        
        [Serializable]
        public class Reference : IAction
        {
            [SerializeField] private ActionComponent actionComponent;
            
            public void Trigger() => this.actionComponent.Trigger();
        }
    }
}