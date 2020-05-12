namespace Chinchillada.Foundation
{
    public interface IRenderer<T> : ISource<T>
    {
        void Render(T content);
    }
}