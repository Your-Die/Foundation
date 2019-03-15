using System;
using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public static class LinkedListExtensions
    {
        /// <summary>
        /// Finds the <see cref="LinkedListNode{T}"/> in the <paramref name="list"/> where the value satisfies the <paramref name="predicate"/>.
        /// </summary> 
        public static LinkedListNode<T> FindNode<T>(this LinkedList<T> list, Func<T, bool> predicate)
        {
            for (var node = list.First; node != null; node = node.Next)
                if (predicate(node.Value))
                    return node;

            return null;
        }

        /// <summary>
        /// Adds the <paramref name="value"/> to the <paramref name="list"/> after the first value that satisfies the <paramref name="predicate"/>.
        /// </summary> 
        public static void AddAfter<T>(this LinkedList<T> list, T value, Func<T, bool> predicate)
        {
            var node = list.FindNode(predicate) ?? list.Last;
            list.AddAfter(node, value);
        }

        /// <summary>
        /// Adds the <paramref name="value"/> to the <paramref name="list"/> before the first value that satisfies the <paramref name="predicate"/>.
        /// </summary> 
        public static void AddBefore<T>(this LinkedList<T> list, T value, Func<T, bool> predicate)
        {
            LinkedListNode<T> node = list.FindNode(predicate) ?? list.Last;
            list.AddBefore(node, value);
        }
    }
}