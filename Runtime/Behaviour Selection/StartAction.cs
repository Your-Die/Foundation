namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class StartAction : AutoRefBehaviour
    {
        [OdinSerialize, Required] private IAction action;

        private void Start()
        {
            base.Awake();
            this.action.Trigger();
        }
    }
}