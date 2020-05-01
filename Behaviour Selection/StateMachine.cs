using Mutiny.Foundation.States;

namespace Mutiny.Foundation.States
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public void TransitionTo(IState state)
        {
            this.CurrentState?.TryExit();

            this.CurrentState = state;

            this.CurrentState?.TryEnter();
        }

        public void Exit() => this.TransitionTo(null);
    }
}