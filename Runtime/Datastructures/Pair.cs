using System;

namespace Chinchillada
{
    /// <summary>
    ///  Simple struct for a pair of values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal struct Pair<T>
    {
        /// <summary>
        /// The first value in this pair.
        /// </summary>
        public T Item1 { get; set; }

        /// <summary>
        /// The second value in this pair.
        /// </summary>
        public T Item2 { get; set; }

        public T Left => Item1;

        public T Right => Item2;

        /// <summary>
        /// Constructs a new pair.
        /// </summary>
        /// <param name="item1"><see cref="Item1"/></param>
        /// <param name="item2"><see cref="Item2"/></param>
        public Pair(T item1, T item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// Applies a predicate to this pair's items.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>the result of the predicate.</returns>
        public bool ApplyPredicate(Func<T, T, bool> predicate)
        {
            return predicate(Item1, Item2);
        }
    }
}