using System;

namespace Chinchillada.Foundation
{
    public interface IListenable<T>
    {
        event Action<T> ValueChanged;
        T Value { get; set; }
    }
}