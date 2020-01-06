using System.Collections.Generic;

namespace Utilities.Algorithms
{
    public interface ISearchProblem<T>
    {
        T InitialState { get; }
        float CalculateHeuristic(T state);
        
        bool IsGoalState(T state);

        IEnumerable<SearchAction<T>> GetSuccessors(T state);
    }

    public class SearchAction<T>
    {
        public T State { get; }
        
        public float Cost { get; }

        public SearchAction(T state, float cost = 1f)
        {
            this.State = state;
            this.Cost = cost;
        }
    }
}