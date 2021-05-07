namespace Chinchillada
{
    public interface IVisualizer<in T>
    {
        void Visualize(T content);
    }
}