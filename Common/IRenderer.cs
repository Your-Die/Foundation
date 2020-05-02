namespace Chinchillada.Foundation
{
    public interface IRenderer<in T>
    {
        void Render(T content);
    }
}