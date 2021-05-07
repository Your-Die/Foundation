namespace Chinchillada
{
    using System.Collections;
    using Chinchillada;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    
    public class RoutineStepper : ChinchilladaBehaviour
    {
        [OdinSerialize] private IRoutine routineSource;

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