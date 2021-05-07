namespace Chinchillada.Common
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}