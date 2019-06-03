using System.Collections.Generic;

namespace Chinchillada.Interactables
{
    public interface ISelector<T>
    {
        T Select(IEnumerable<T> items);
    }
}