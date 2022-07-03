namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class RevertableActionComponent : AutoRefBehaviour, IRevertableAction
    {
        [OdinSerialize, Required] private IRevertableAction action;
        
        public void Trigger() => this.action.Trigger();

        public void Revert() => this.action.Revert();
    }
}