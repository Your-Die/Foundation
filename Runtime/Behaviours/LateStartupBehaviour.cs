namespace Chinchillada
{
    public abstract class LateStartupBehaviour  : AutoRefBehaviour
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