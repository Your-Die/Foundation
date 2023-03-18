using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    public class ConditionComponent : MonoBehaviour, ICondition
    {
        [SerializeReference, Required] private ICondition condition;

        public bool Validate() => this.condition.Validate();

        [Serializable]
        public class Reference : ICondition
        {
            [SerializeField] private ConditionComponent component;
            
            public bool Validate() => this.component.Validate();
        }
    }
}