using System;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// <see cref="IPresenter{T}"/> that presents one option out of multiple,
    /// invoking <see cref="SelectedEvent"/> when it is selected.
    /// </summary>
    public interface IOptionPresenter<T> : IPresenter<T>, IPoolable
    {
        event Action<T> SelectedEvent;
    }
}