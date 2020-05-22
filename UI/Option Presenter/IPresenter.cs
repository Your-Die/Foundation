using System.Collections.Generic;

namespace Chinchillada.Foundation.UI
{
    using System;
    using Chinchillada;
    using Chinchillada.Foundation;

    /// <summary>
    /// Interface for UI presenters.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Content"/> being presented.</typeparam>
    public interface IPresenter<T> : IComponent
    {
        /// <summary>
        /// Presents the <paramref name="content"/>.
        /// </summary>
        void Present(T content);

        /// <summary>
        /// Hide this <see cref="IPresenter{T}"/>.
        /// </summary>
        void Hide();
    }

    /// <summary>
    /// <see cref="IPresenter{T}"/> that presents multiple options of a given type,
    /// and invokes <see cref="SelectedEvent"/> when one of those option sis selected.
    /// </summary>
    public interface IMultipleChoicePresenter<T> : IPresenter<IEnumerable<T>>
    {
        event Action<T> SelectedEvent;
    }

    /// <summary>
    /// <see cref="IPresenter{T}"/> that presents one option out of multiple,
    /// invoking <see cref="SelectedEvent"/> when it is selected.
    /// </summary>
    public interface IOptionPresenter<T> : IPresenter<T>, IPoolable
    {
        event Action<T> SelectedEvent;
    }       
}