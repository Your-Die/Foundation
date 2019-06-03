using System.Collections.Generic;
using System.Linq;
using Chinchillada.Interactables;
using Chinchillada.Utilities;

namespace Utilities.Common
{
    public class UtilitySelector<T> : ISelector<T>
    {
        private readonly LazyList<IScorer<T>> _scorers;

        public UtilitySelector(IEnumerable<IScorer<T>> scorers)
        {
            _scorers = new LazyList<IScorer<T>>(scorers);
        }

        public T Select(IEnumerable<T> items)
        {
            return items.Best(Score);

            float Score(T item) => _scorers.Sum(scorer => scorer.Evaluate(item));
        }
    }
}