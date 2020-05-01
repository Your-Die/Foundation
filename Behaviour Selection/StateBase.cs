namespace Mutiny.Foundation.States
{
    public abstract class StateBase : IState
    {
        public bool IsActive { get; private set; }
        public virtual void Enter() => this.IsActive = true;

        public virtual void Exit() => this.IsActive = false;
    }
}