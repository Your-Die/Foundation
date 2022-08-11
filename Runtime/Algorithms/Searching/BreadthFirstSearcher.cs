namespace Chinchillada.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class BreadthFirstSearcher : IPathFinder
    {
        public IEnumerable<T> FindPath<T>(ISearchProblem<T> problem) 
        {
            // Setup data-structures.
            var frontier = new Queue<T>();
            var cameFrom = new Dictionary<T, T>();

            // Add initial state.
            frontier.Enqueue(problem.InitialState);
            cameFrom[problem.InitialState] = default;

            while (frontier.Any())
            {
                var state = frontier.Dequeue();

                // Goal test.
                if (problem.IsGoalState(state))
                    return Path.Build(problem.InitialState, state, cameFrom).Reverse();

                // Add new successors.
                var successors = problem.GetSuccessors(state);
                foreach (var successor in successors)
                {
                    if (cameFrom.ContainsKey(successor.State))
                        continue;

                    cameFrom[successor.State] = successor.State;
                    frontier.Enqueue(successor.State);
                }
            }

            return null;
        }
    }
}