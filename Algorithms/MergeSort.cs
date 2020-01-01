using System.Collections.Generic;

namespace Utilities.Algorithms
{
    public class MergeSort
    {
        public static IEnumerable<T> Merge<T>(IEnumerable<T> left, IEnumerable<T> right, IComparer<T> comparer)
        {
            using (var leftEnumerator = left.GetEnumerator())
            using (var rightEnumerator = right.GetEnumerator())
            {
                var leftAny = leftEnumerator.MoveNext();
                var rightAny = rightEnumerator.MoveNext();

                while (leftAny && rightAny)
                {
                    var comparison = comparer.Compare(leftEnumerator.Current, rightEnumerator.Current);
                    if (comparison < 0)
                    {
                        yield return leftEnumerator.Current;
                        leftAny = leftEnumerator.MoveNext();
                    }
                    else
                    {
                        yield return rightEnumerator.Current;
                        rightAny = rightEnumerator.MoveNext();
                    }
                }

                while (leftAny)
                {
                    yield return leftEnumerator.Current;
                    leftAny = leftEnumerator.MoveNext();
                }

                while (rightAny)
                {
                    yield return rightEnumerator.Current;
                    rightAny = rightEnumerator.MoveNext();
                }
            }
        }
    }
}