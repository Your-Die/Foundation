namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class EnableDisableActionCaller : AutoRefBehaviour
    {
        [OdinSerialize, Required] private IRevertableAction action;

        private void OnEnable() => this.action.Trigger();

        private void OnDisable() => this.action.Revert();
    }
}