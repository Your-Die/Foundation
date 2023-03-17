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

            foreach (var initialState in problem.InitialStates)
            {
                var node = new SearchNode<T>(initialState, 0);

                frontier.Enqueue(node);
                cameFrom[initialState]  = default;
                costSoFar[initialState] = 0;
            }
            
  

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return Path.Build(problem.InitialStates, node.State, cameFrom);

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