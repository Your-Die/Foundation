using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation.States
{
    public interface ISequentialStateMachine : IList<ISequentialState>, IState
    {
        int CurrentIndex { get; }

        void TransitionTo(int index);
        bool TryTransitionTo(int index);
        event Action BorderReached;
    }
}