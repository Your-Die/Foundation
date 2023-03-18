    using System;
    using System.Collections;
    using UnityEngine;

    namespace Chinchillada.Behavior
    {
        [Serializable]
        public class ActionExecutable : IExecutable
        {
            [SerializeReference] private IAction action;
        
            public IEnumerator Execute()
            {
                this.action.Trigger();
                yield return null;
            }
        }
    }