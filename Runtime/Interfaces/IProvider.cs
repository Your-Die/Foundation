using System.Collections.Generic;

namespace Chinchillada.Foundation
{
    public interface IProvider<T>
    {
        IEnumerable<T> Provide();
    }
}