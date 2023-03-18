using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{

    [Serializable]
    public class BranchAction : IAction
    {
        [SerializeReference, Required] private ICondition condition;
        
        [SerializeReference, Required] private IAction actionOnTrue;
        [SerializeReference, Required] private IAction actionOnFalse;

        public BranchAction(ICondition condition, IAction actionOnTrue, IAction actionOnFalse)
        {
            this.condition = condition;
            this.actionOnTrue = actionOnTrue;
            this.actionOnFalse = actionOnFalse;
        }

        public void Trigger()
        {
            var action = this.condition.Validate() 
                ? this.actionOnTrue 
                : this.actionOnFalse;
            
            action?.Trigger();
        }
    }
}