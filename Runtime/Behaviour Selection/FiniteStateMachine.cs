using System;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    [Serializable]
    public class FiniteStateMachine
    {
        [ShowInInspector]
        public IFiniteState CurrentFiniteState { get; private set; }

        public FiniteStateMachine()
        {
        }

        public FiniteStateMachine(IFiniteState initialFiniteState) => this.TransitionTo(initialFiniteState);

        public void TransitionTo(IFiniteState finiteState)
        {
            this.CurrentFiniteState?.TryExit();

            this.CurrentFiniteState = finiteState;

            this.CurrentFiniteState?.TryEnter();
        }

        public void Exit() => this.TransitionTo(null);
    }

    [Serializable]
    public class StateMachine<T>
    {
        [ShowInInspector]
        public IState<T> CurrentState { get; private set; }
        
        public StateMachine()
        {
        }

        public StateMachine(IState<T> initialState, T context) => this.TransitionTo(initialState, context);

        public void TransitionTo(IState<T> state, T context)
        {
            if (this.CurrentState != null && this.CurrentState.IsActive) 
                this.CurrentState.Exit(context);

            this.CurrentState = state;

            if (this.CurrentState != null && this.CurrentState.IsActive) 
                this.CurrentState.Enter(context);
        }

        public void Exit(T context = default) => this.TransitionTo(null, context);
    }
}