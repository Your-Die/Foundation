namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class AwakeAction : AutoRefBehaviour
    {
        [OdinSerialize, Required] private IAction action;

        protected override void Awake()
        {
            base.Awake();
            this.action.Trigger();
        }
    }
}