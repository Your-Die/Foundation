namespace Chinchillada
{
    public interface IFactory<out T>
    {
        T Create();
    }
}