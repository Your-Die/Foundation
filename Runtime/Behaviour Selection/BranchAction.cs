namespace Chinchillada.Behavior
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class BranchAction : IAction
    {
        [OdinSerialize, Required] private ICondition condition;
        
        [OdinSerialize, Required] private IAction actionOnTrue;
        [OdinSerialize, Required] private IAction actionOnFalse;
        
        public void Trigger()
        {
            var action = this.condition.Validate() 
                ? this.actionOnTrue 
                : this.actionOnFalse;
            
            action?.Trigger();
        }
    }
}