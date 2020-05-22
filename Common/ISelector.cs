using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public interface ISelector<T>
    {
        T Select(IEnumerable<T> items);
    }
}