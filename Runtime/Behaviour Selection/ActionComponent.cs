namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class ActionComponent : ActionComponentBase
    {
        [OdinSerialize, Required] private IAction action;

        public IAction Action
        {
            set => this.action = value;
        }

        [Button]
        public override void Trigger() => this.action.Trigger();
    }
}