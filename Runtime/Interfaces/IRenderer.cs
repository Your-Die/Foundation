namespace Chinchillada.Foundation
{
    public interface IRenderer<T>
    {
        void Render(T content);
    }
}