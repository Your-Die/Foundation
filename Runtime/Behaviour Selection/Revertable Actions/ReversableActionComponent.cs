using System;
using UnityEngine;

namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;

    public class ReversableActionComponent : MonoBehaviour, IRevertableAction
    {
        [SerializeReference, Required] private IRevertableAction action;
        
        public void Trigger() => this.action.Trigger();

        public void Revert() => this.action.Revert();

        [Serializable]
        public class Reference : IRevertableAction
        {
            [SerializeField] private ReversableActionComponent component;
            
            public void Trigger() => this.component.Trigger();
            public void Revert() => this.component.Revert();
        }
    }
}