namespace Chinchillada.Common
{
    public interface IScorer<T>
    {
        float Evaluate(T item);
    }
}