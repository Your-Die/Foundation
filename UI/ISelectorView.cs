using System;

namespace Mutiny.Foundation.UI
{
    public interface ISelectorView<out T>
    {
        event Action<T> Selected;
    }
}