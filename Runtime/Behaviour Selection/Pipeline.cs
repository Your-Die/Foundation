namespace Chinchillada.Behavior
{
    using System.Collections;
    using System.Collections.Generic;
    using Chinchillada;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Pipeline : MonoBehaviour, IExecutable
    {
        [SerializeReference] private List<IExecutable> executables;

        private RoutineHandler routineHandler;

        private void Awake() => this.routineHandler = new RoutineHandler(this);

        [Button]
        public void StartExecuting()
        {
            this.routineHandler.StartRoutine(this.Execute());
        }
        
        public IEnumerator Execute()
        {
            foreach (var executable in this.executables)
                yield return executable.Execute();
        }
    }
}