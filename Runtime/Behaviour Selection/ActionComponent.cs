namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class ActionComponent : AutoRefBehaviour, IAction
    {
        [OdinSerialize, Required] private IAction action;

        [Button]
        public void Trigger()
        {
            this.action.Trigger();
        }
    }
}