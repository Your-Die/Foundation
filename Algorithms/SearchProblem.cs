using System.Collections.Generic;

namespace Utilities.Algorithms
{
    /// <summary>
    /// Definition of a search problem.
    /// </summary>
    public interface ISearchProblem<T>
    {
        /// <summary>
        /// Initial state of the problem.
        /// </summary>
        T InitialState { get; }
        
        /// <summary>
        /// Calculates the heuristic of the <paramref name="state"/>.
        /// </summary>
        float CalculateHeuristic(T state);

        /// <summary>
        /// Checks if the <paramref name="state"/> is a goal state.
        /// </summary>
        bool IsGoalState(T state);

        /// <summary>
        /// Generates the successor states of <paramref name="state"/>.
        /// </summary>
        IEnumerable<SearchAction<T>> GetSuccessors(T state);
    }

    /// <summary>
    /// An action within <see cref="ISearchProblem{T}"/>.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public class SearchAction<TState>
    {
        /// <summary>
        /// The resulting state.
        /// </summary>
        public TState State { get; }
        
        /// <summary>
        /// The cost of performing this <see cref="SearchAction{TState}"/>.
        /// </summary>
        public float Cost { get; }

        public SearchAction(TState state, float cost = 1f)
        {
            this.State = state;
            this.Cost = cost;
        }
    }
}