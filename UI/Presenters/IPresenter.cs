namespace Chinchillada.Foundation.UI
{
    using Chinchillada;

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
}