namespace Chinchillada.PCGraph
{
    public interface IFactory<out T>
    {
        T Create();
    }
}