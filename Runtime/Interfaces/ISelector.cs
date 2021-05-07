using System.Collections.Generic;

namespace Chinchillada
{
    public interface ISelector<T>
    {
        T Select(IEnumerable<T> items);
    }
}