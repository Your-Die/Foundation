using System;
using System.Collections.Generic;

namespace Mutiny.Foundation.SSM
{
    public interface ISequentialStateMachine : IList<ISequentialState>, IState
    {
        int CurrentIndex { get; }

        void TransitionTo(int index);
        bool TryTransitionTo(int index);
        event Action BorderReached;
    }
}