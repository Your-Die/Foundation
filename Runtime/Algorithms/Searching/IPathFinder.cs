namespace Chinchillada.Algorithms
{
    using System;
    using System.Collections.Generic;

    public interface IPathFinder
    {
        IEnumerable<T> FindPath<T>(ISearchProblem<T> problem);
    }
}