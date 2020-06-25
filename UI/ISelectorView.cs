using System;

namespace Chinchillada.Foundation.UI
{
    public interface ISelectorView<out T>
    {
        event Action<T> Selected;
    }
}