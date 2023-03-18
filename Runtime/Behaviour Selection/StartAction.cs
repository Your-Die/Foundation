using UnityEngine;

namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class StartAction : AutoRefBehaviour
    {
        [SerializeReference, Required] private IAction action;

        private void Start()
        {
            base.Awake();
            this.action.Trigger();
        }
    }
}