using System.Collections.Generic;

namespace Chinchillada.Algorithms
{
    public class AStarPathfinder : IPathFinder
    {
        public IEnumerable<T> FindPath<T>(ISearchProblem<T> problem)
        {
            var frontier     = new CustomPriorityQueue<SearchNode<T>>();
            var predecessors = new Dictionary<T, T>();
            var costs        = new Dictionary<T, float>();

            var initialNode = new SearchNode<T>(problem.InitialState, 0);
            frontier.Enqueue(initialNode);

            predecessors[problem.InitialState] = default;
            costs[problem.InitialState]        = 0;

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return Path.Build(problem.InitialState, node.State, predecessors);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    var newCost = costs[node.State] + successor.Cost;
                    if (costs.TryGetValue(successor.State, out var cost) && cost < newCost)
                        continue;

                    costs[successor.State]        = newCost;
                    predecessors[successor.State] = node.State;

                    var heuristic = problem.CalculateHeuristic(successor.State);
                    var priority  = newCost + heuristic;

                    var successorNode = new SearchNode<T>(successor.State, priority);
                    frontier.Enqueue(successorNode);
                }
            }

            return null;
        }
    }
}