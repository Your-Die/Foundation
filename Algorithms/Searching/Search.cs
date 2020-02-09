using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;

namespace Utilities.Algorithms
{
    public static class Search
    {
        public static IEnumerable<T> BreadthFirst<T>(ISearchProblem<T> problem)
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
                    return BuildPath(problem.InitialState, state, cameFrom).Reverse();

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

        public static IEnumerable<T> UniformCost<T>(ISearchProblem<T> problem)
        {
            var frontier = new PriorityQueue<SearchNode<T>>();
            var cameFrom = new Dictionary<T, T>();
            var costSoFar = new Dictionary<T, float>();

            var firstNode = new SearchNode<T>(problem.InitialState, 0);

            frontier.Enqueue(firstNode);
            cameFrom[problem.InitialState] = default;
            costSoFar[problem.InitialState] = 0;

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return BuildPath(problem.InitialState, node.State, cameFrom);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    var newCost = costSoFar[node.State] + successor.Cost;

                    if (costSoFar.TryGetValue(successor.State, out var cost) && newCost >= cost)
                        continue;

                    var action = new SearchNode<T>(successor.State, newCost);
                    frontier.Enqueue(action);

                    costSoFar[successor.State] = newCost;
                    cameFrom[successor.State] = node.State;
                }
            }

            return null;
        }

        public static IEnumerable<T> GreedyBestFirst<T>(ISearchProblem<T> problem)
        {
            var frontier = new PriorityQueue<SearchNode<T>>();
            var cameFrom = new Dictionary<T, T>();

            var initialNode = new SearchNode<T>(problem.InitialState, 0);

            frontier.Enqueue(initialNode);
            cameFrom[problem.InitialState] = default;

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return BuildPath(problem.InitialState, node.State, cameFrom);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    if (cameFrom.ContainsKey(successor.State))
                        continue;

                    var priority = problem.CalculateHeuristic(successor.State);
                    var successorNode = new SearchNode<T>(successor.State, priority);

                    frontier.Enqueue(successorNode);
                    cameFrom[successor.State] = node.State;
                }
            }

            return null;
        }

        public static IEnumerable<T> AStar<T>(ISearchProblem<T> problem)
        {
            var frontier = new PriorityQueue<SearchNode<T>>();
            var predecessors = new Dictionary<T, T>();
            var costs = new Dictionary<T, float>();
            
            var initialNode = new SearchNode<T>(problem.InitialState, 0);
            frontier.Enqueue(initialNode);
            
            predecessors[problem.InitialState] = default;
            costs[problem.InitialState] = 0;

            while (frontier.Any())
            {
                var node = frontier.Dequeue();

                if (problem.IsGoalState(node.State))
                    return BuildPath(problem.InitialState, node.State, predecessors);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    var newCost = costs[node.State] + successor.Cost;
                    if (costs.TryGetValue(successor.State, out var cost) && cost < newCost)
                        continue;

                    costs[successor.State] = newCost;
                    predecessors[successor.State] = node.State;

                    var heuristic = problem.CalculateHeuristic(successor.State);
                    var priority = newCost + heuristic;
                    
                    var successorNode = new SearchNode<T>(successor.State, priority);
                    frontier.Enqueue(successorNode);
                }
            }

            return null;
        }

        private static IEnumerable<T> BuildPath<T>(T start, T end, IReadOnlyDictionary<T, T> actions)
        {
            var current = end;
            var comparer = Comparer<T>.Default;

            while (comparer.Compare(current, start) != 0)
            {
                yield return current;
                current = actions[current];
            }

            yield return start;
        }
    }
}