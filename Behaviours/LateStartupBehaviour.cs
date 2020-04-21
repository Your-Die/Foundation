using Chinchillada;

namespace Mutiny.Foundation.Behaviours
{
    public abstract class LateStartupBehaviour  : ChinchilladaBehaviour
    {
        private bool hasStarted;

        protected abstract void LateEnable();
        
        protected virtual void Start()
        {
            this.hasStarted = true;
            this.LateEnable();
        }

        protected void OnEnable()
        {
            if (this.hasStarted) 
                this.LateEnable();
        }
    }
}