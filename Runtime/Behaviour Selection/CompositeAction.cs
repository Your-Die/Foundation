using System;
using UnityEngine;
using System.Collections.Generic;

namespace Chinchillada.Behavior
{

    [Serializable]
    public class CompositeAction : IAction
    {
        [SerializeReference, FindNestedComponents] private List<IAction> actions;

        public CompositeAction(List<IAction> actions)
        {
            this.actions = actions;
        }

        public void Trigger()
        {
            foreach (var action in this.actions) 
                action.Trigger();
        }
    }
}