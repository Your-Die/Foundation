namespace Chinchillada.Foundation.Common
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}