using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public interface IPoolList<TItem> : IReadOnlyList<TItem>
    {
        /// <summary>
        /// Event invoked when a new item is activated.
        /// </summary>
        event Action<TItem> ItemActivated;

        /// <summary>
        /// Event invoked when an item is deactivated.
        /// </summary>
        event Action<TItem> ItemDeactivated;

        /// <summary>
        /// Applies the <paramref name="action"/> for each item in <paramref name="list"/>,
        /// with an item from this <see cref="PoolListBase{T}"/>.
        /// </summary>
        /// <returns>The change in active items in this <see cref="PoolListBase{T}"/>.</returns>
        int ApplyWith<TOther>(IList<TOther> list, Action<TOther, TItem> action);

        /// <summary>
        /// Applies the <paramref name="action"/> for each item in <paramref name="list"/> with associated index,
        /// with an item from this <see cref="PoolListBase{T}"/>.
        /// </summary>
        /// <returns>The change in active items in this <see cref="PoolListBase{T}"/>.</returns>
        int ApplyWith<TOther>(IList<TOther> list, Action<int, TOther, TItem> action);

        int Scope(int count);
        void AddEmptyItem();
        void Clear();
        TItem Acquire();
        int IndexOf(TItem item);
    }
}