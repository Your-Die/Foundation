using System.Collections.Generic;

namespace Chinchillada
{
    public interface IProvider<T>
    {
        IEnumerable<T> Provide();
    }
}