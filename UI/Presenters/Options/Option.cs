using Mutiny.Thesis.UI;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// Generic class for <see cref="IOption"/> being presented in a <see cref="OptionPresenter"/>.
    /// </summary>
    /// <typeparam name="T">The type of content being encapsulated by this option.</typeparam>
    public class Option<T> : IOption
    {
        public T Content { get; }

        public Option(T content)
        {
            this.Content = content;
        }

        /// <summary>
        /// Present this <see cref="Option{T}"/> in the <paramref name="button"/>.
        /// </summary>
        public virtual void Present(ButtonController button)
        {
            button.TextElement.text = this.Content.ToString();
        }

        /// <summary>
        /// Extract the <see cref="Content"/> from the <paramref name="option"/>.
        /// </summary>
        public static T GetContent(IOption option)
        {
            var asBase = (Option<T>) option;
            return asBase.Content;
        }
    }
}