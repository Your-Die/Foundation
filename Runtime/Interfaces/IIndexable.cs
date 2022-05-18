using System.Collections.Generic;

namespace Chinchillada.Common
{
    public interface IIndexable<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }
    }
}