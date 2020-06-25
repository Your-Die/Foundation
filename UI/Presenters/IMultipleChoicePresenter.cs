using System;
using System.Collections.Generic;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// <see cref="IPresenter{T}"/> that presents multiple options of a given type,
    /// and invokes <see cref="SelectedEvent"/> when one of those option sis selected.
    /// </summary>
    public interface IMultipleChoicePresenter<T> : IPresenter<IEnumerable<T>>
    {
        event Action<T> SelectedEvent;
    }
}