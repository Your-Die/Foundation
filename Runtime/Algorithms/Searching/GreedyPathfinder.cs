namespace Chinchillada.Algorithms
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class GreedyPathfinder : IPathFinder
    {
        public IEnumerable<T> FindPath<T>(ISearchProblem<T> problem)
        {
            var frontier = new CustomPriorityQueue<SearchNode<T>>();
            var cameFrom = new Dictionary<T, T>();

            foreach (var initialState in problem.InitialStates)
            {
                var initialNode = new SearchNode<T>(initialState, 0);

                frontier.Enqueue(initialNode);
                cameFrom[initialState] = default;
            }

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return Path.Build(problem.InitialStates, node.State, cameFrom);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    if (cameFrom.ContainsKey(successor.State))
                        continue;

                    var priority      = problem.CalculateHeuristic(successor.State);
                    var successorNode = new SearchNode<T>(successor.State, priority);

                    frontier.Enqueue(successorNode);
                    cameFrom[successor.State] = node.State;
                }
            }

            return null;
        }
    }
}