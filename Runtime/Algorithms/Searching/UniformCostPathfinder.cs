namespace Chinchillada.Algorithms
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class UniformCostPathfinder : IPathFinder
    {
        public IEnumerable<T> FindPath<T>(ISearchProblem<T> problem) 
        {
            var frontier  = new CustomPriorityQueue<SearchNode<T>>();
            var cameFrom  = new Dictionary<T, T>();
            var costSoFar = new Dictionary<T, float>();

            var firstNode = new SearchNode<T>(problem.InitialState, 0);

            frontier.Enqueue(firstNode);
            cameFrom[problem.InitialState]  = default;
            costSoFar[problem.InitialState] = 0;

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return Path.Build(problem.InitialState, node.State, cameFrom);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    var newCost = costSoFar[node.State] + successor.Cost;

                    if (costSoFar.TryGetValue(successor.State, out var cost) && newCost >= cost)
                        continue;

                    var action = new SearchNode<T>(successor.State, newCost);
                    frontier.Enqueue(action);

                    costSoFar[successor.State] = newCost;
                    cameFrom[successor.State]  = node.State;
                }
            }

            return null;
        }
    }
}