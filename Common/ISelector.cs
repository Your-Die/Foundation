using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public interface ISelector<T>
    {
        T Select(IEnumerable<T> items);
    }
}