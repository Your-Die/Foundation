namespace Mutiny.Thesis.UI
{
    /// <summary>
    /// Common class for options being presented in a <see cref="OptionPresenter"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Option<T> : IOption
    {
        public Option(T content)
        {
            this.Content = content;
        }

        public T Content { get; }

        public virtual void Present(ButtonController button)
        {
            button.TextElement.text = this.Content.ToString();
        }

        public static T GetContent(IOption option)
        {
            var asBase = (Option<T>) option;
            return asBase.Content;
        }
    }
}