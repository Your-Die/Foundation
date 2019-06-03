using System.Collections.Generic;

namespace Chinchillada.Interactables
{
    public interface IProvider<T>
    {
        IEnumerable<T> Provide();
    }
}