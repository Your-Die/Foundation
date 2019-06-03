using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public interface IProvider<T>
    {
        IEnumerable<T> Provide();
    }
}