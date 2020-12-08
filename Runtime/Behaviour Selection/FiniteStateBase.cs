namespace Chinchillada.Behavior
{
    public abstract class FiniteStateBase : IFiniteState
    {
        public bool IsActive { get; private set; }
        public virtual void Enter() => this.IsActive = true;

        public virtual void Exit() => this.IsActive = false;
    }
}