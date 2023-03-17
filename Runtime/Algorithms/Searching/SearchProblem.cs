using System;
using System.Collections.Generic;

namespace Chinchillada.Algorithms
{
    /// <summary>
    /// Definition of a search problem.
    /// </summary>
    public interface ISearchProblem<T>
    {
        /// <summary>
        /// Initial state of the problem.
        /// </summary>
        IReadOnlyList<T> InitialStates { get; }
        
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
        IEnumerable<SearchNode<T>> GetSuccessors(T state);
    }

    /// <summary>
    /// An action within <see cref="ISearchProblem{T}"/>.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public class SearchNode<TState> : IComparable, IComparable<SearchNode<TState>>
    {
        /// <summary>
        /// The resulting state.
        /// </summary>
        public TState State { get; }
        
        /// <summary>
        /// The cost of performing this <see cref="SearchNode{TState}"/>.
        /// </summary>
        public float Cost { get; }

        public SearchNode(TState state, float cost = 1f)
        {
            this.State = state;
            this.Cost = cost;
        }

        public int CompareTo(object obj)
        {
            var otherNode = (SearchNode<TState>) obj;
            return this.Cost.CompareTo(otherNode.Cost);
        }

        public int CompareTo(SearchNode<TState> other)
        {
            return this.Cost.CompareTo(other.Cost);
        }
    }
}