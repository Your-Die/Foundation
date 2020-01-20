using System;
using System.Collections.Generic;
using Chinchillada.Utilities;

namespace Utilities.Algorithms
{
    // todo: this isn't actually Astar, need to rewrite.
    public static class AStar
    {
        public static IEnumerable<T> Search<T>(ISearchProblem<T> problem)
        {
            var frontier = new PriorityQueue<SearchNode<T>>();
            var openList = new HashSet<T>();
            var explored = new Dictionary<T, SearchNode<T>>();
            
            var heuristic = problem.CalculateHeuristic(problem.InitialState);
            var firstNode = new SearchNode<T>(problem.InitialState, heuristic, null);

            openList.Add(problem.InitialState);
            frontier.Enqueue(firstNode);
            
            while (frontier.HasNext)
            {
                var node = frontier.Dequeue();
                
                openList.Remove(node.State);
                explored[node.State] = node;

                if (problem.IsGoalState(node.State))
                    return BuildPath(node, explored);

                var successors = problem.GetSuccessors(node.State);
                foreach (var successor in successors)
                {
                    var successorState = successor.State;
                    if (explored.ContainsKey(successorState) || openList.Contains(successorState))
                        continue;

                    var totalCost = successor.Cost + problem.CalculateHeuristic(successorState);
                    var nextNode = new SearchNode<T>(successorState, totalCost, node);
                    
                    frontier.Enqueue(nextNode);
                    openList.Add(successorState);
                }
            }

            return null;
        }

        private static IEnumerable<T> BuildPath<T>(SearchNode<T> node, Dictionary<T,SearchNode<T>> explored)
        {
            var path = new LinkedList<T>();
            path.AddFirst(node.State);
            
            while (node.PreviousNode != null)
            {
                node = node.PreviousNode;
                
                var state = node.State;
                path.AddFirst(state);
                
                node = explored[state];
            }

            return path;
        }
        
        private class SearchNode<T>  :IPriorityNode, IComparable<IPriorityNode>, IComparable
        {
            public SearchNode<T> PreviousNode { get; }
            
            public T State { get; }

            public float Priority { get; }

            public SearchNode(T state, float priority, SearchNode<T> previousNode)
            {
                this.State = state;
                this.Priority = priority;
                this.PreviousNode = previousNode;
            }

            public int CompareTo(IPriorityNode other) => this.Priority.CompareTo(other.Priority);
            public int CompareTo(object obj) => this.CompareTo((SearchNode<T>) obj);
        }

        private interface IPriorityNode
        {
            float Priority { get; }
        }
    }
}