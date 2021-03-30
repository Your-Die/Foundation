namespace Chinchillada.Foundation
{
    public interface IVisualizer<in T>
    {
        void Visualize(T content);
    }
}