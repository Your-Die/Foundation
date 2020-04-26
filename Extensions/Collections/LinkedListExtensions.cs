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

        public static IEnumerable<T> RemoveWhere<T>(this LinkedList<T> list, Predicate<T> predicate)
        {
            for (var node = list.First; node != null;)
            {
                var next = node.Next;

                if (predicate(node.Value))
                {
                    list.Remove(node);
                    yield return node.Value;
                }

                node = next;
            }
        }

        /// <summary>
        /// Removes all elements in the <paramref name="list"/> that satisfy the <paramref name="predicate"/> and yields them.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="list"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> we want to remove elements from.</param>
        /// <param name="predicate">The predicate that is applied to remove elements from <paramref name="list"/>.</param>
        /// <returns>The removed elements.</returns>
        public static IEnumerable<T> RemoveAll<T>(this LinkedList<T> list, Func<T, bool> predicate)
        {
            for (var node = list.First; node != null;)
            {
                var next = node.Next;

                if (predicate(node.Value))
                {
                    list.Remove(node);
                    yield return node.Value;
                }

                node = next;
            }
        }

        public static IEnumerable<T> FilterIterate<T>(this LinkedList<T> list)
        {
            for (var node = list.First; node != null;)
            {
                var next = node.Next;

                if (node.Value == null)
                {
                    list.Remove(node);
                }
                else
                {
                    yield return node.Value;
                }

                node = next;
            }
        }

        public static T GrabFirst<T>(this LinkedList<T> list)
        {
            var first = list.First;
            list.RemoveFirst();

            return first.Value;
        }
    }
}