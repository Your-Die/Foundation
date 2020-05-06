using System;
using Sirenix.OdinInspector;

namespace Mutiny.Foundation.States
{
    [Serializable]
    public class StateMachine
    {
        [ShowInInspector]
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