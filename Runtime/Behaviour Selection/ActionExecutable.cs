    using System;
    using System.Collections;
    using Sirenix.Serialization;

    namespace Chinchillada.Behavior
    {
        [Serializable]
        public class ActionExecutable : IExecutable
        {
            [OdinSerialize] private IAction action;
        
            public IEnumerator Execute()
            {
                this.action.Trigger();
                yield return null;
            }
        }
    }