namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class ConditionComponent : AutoRefBehaviour, ICondition
    {
        [OdinSerialize, Required] private ICondition condition;

        public bool Validate() => this.condition.Validate();
    }
}