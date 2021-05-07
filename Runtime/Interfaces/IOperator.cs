namespace Chinchillada
{
    public interface IOperator<T>
    {
        T Execute(T left, T right);
    }
}