using UnityEngine;

namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;

    public class ActionComponent : ActionComponentBase
    {
        [SerializeReference, Required, FindNestedComponents] private IAction action;

        public override void Trigger() => this.action.Trigger();
    }
}