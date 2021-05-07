using System.Collections;
using UnityEngine;

namespace Chinchillada
{
    public class RoutineHandler
    {
        private MonoBehaviour currentContext;
        private IEnumerator currentRoutine;

        public RoutineHandler(MonoBehaviour context)
        {
            this.currentContext = context;
        }

        public void StartRoutine(IEnumerator routine)
        {
            this.Stop();

            this.currentRoutine = routine;
            
            this.Start();
        }
        
        public void StartRoutine(MonoBehaviour context, IEnumerator routine)
        {
            this.Stop();

            this.currentContext = context;
            this.currentRoutine = routine;

            this.Start();
        }

        public void Stop()
        {
            if (this.currentContext != null && this.currentRoutine != null)
                this.currentContext.StopCoroutine(this.currentRoutine);
        }

        private void Start()
        {
            this.currentContext.StartCoroutine(this.currentRoutine);
        }
    }
}