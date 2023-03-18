using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Chinchillada
{
    public class RoutineStepper : MonoBehaviour
    {
        [SerializeReference] private IRoutine routineSource;

        private IEnumerator routine;
        private bool        canStep;
        
        [Button]
        private void Step()
        {
            if (this.routine == null)
            {
                this.routine = this.routineSource.Routine();
                this.canStep = true;
            }

            if (this.canStep && !this.routine.MoveNext())
            {
                this.canStep = false;
            }
        }


        [Button]
        private void Reset()
        {
            this.routine = null;
        }
    }
}