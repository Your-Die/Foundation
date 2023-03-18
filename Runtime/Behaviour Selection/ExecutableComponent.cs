using System;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    public class ExecutableComponent : MonoBehaviour, IExecutable
    {
        [SerializeReference, Required] private IExecutable executable;

        public IEnumerator Execute() => this.executable.Execute();

        [Serializable]
        public class Reference : IExecutable
        {
            [SerializeField] private ExecutableComponent component;
            
            public IEnumerator Execute() => this.component.Execute();
        }
    }
}