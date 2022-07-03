namespace Chinchillada.Behavior
{
    using System.Collections.Generic;
    using Sirenix.Serialization;

    public class CompositeAction : IAction
    {
        [OdinSerialize, FindNestedComponents] private List<IAction> actions;
        
        public void Trigger()
        {
            foreach (var action in this.actions) 
                action.Trigger();
        }
    }
}