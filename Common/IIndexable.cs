using System.Collections.Generic;

namespace Utilities.Common
{
    public interface IIndexable<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }
    }
}