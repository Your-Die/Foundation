namespace Mutiny.Foundation.Common
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}