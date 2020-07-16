﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Foundation
{
    public static class ListExtensions
    {
        
        public static int LastIndex<T>(this IList<T> list)
        {
            return list.Count - 1;
        }

        public static T ExtractLast<T>(this IList<T> list)
        {
            if (list.IsEmpty())
                return default;

            var index = list.LastIndex();
            var item = list[index];
            list.RemoveAt(index);

            return item;
        }

        public static T Extract<T>(this IList<T> list, int index)
        {
            var element = list[index];
            list.RemoveAtQuick(index);

            return element;
        }

        public static bool ContainsAll<T>(this IList<T> collection, IEnumerable<T> requiredItems)
        {
            return requiredItems.All(collection.Contains);
        }
        
        public static IEnumerable<int> GetIndices<T>(this IList<T> list)
        {
            for (var index = 0; index < list.Count; index++)
                yield return index;
        }

        
        public static IEnumerable<int> IndicesWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var index = 0; index < list.Count; index++)
            {
                var item = list[index];
                if (predicate(item))
                    yield return index;
            }
        }

        public static bool RemoveQuick<T>(this IList<T> list, T element)
        {
            var index = list.IndexOf(element);
            if (index == -1)
                return false;

            list.RemoveAtQuick(index);
            return true;
        }

        public static void RemoveAtQuick<T>(this IList<T> list, int index)
        {
            var lastIndex = list.LastIndex();
            list.SwapElements(index, lastIndex);

            list.RemoveAt(lastIndex);
        }

        public static void SwapElements<T>(this IList<T> list, int index1, int index2)
        {
            var temp = list[index1];

            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}