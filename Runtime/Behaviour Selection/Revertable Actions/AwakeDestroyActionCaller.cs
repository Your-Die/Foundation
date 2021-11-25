namespace Chinchillada.Behavior
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class AwakeDestroyActionCaller : ChinchilladaBehaviour
    {
        [OdinSerialize, Required] private IRevertableAction action;

        protected override void Awake()
        {
            base.Awake();
            this.action.Trigger();
        }

        private void OnDestroy() => this.action.Revert();
    }
}