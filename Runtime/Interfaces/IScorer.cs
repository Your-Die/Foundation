namespace Chinchillada
{
    public interface IScorer<T>
    {
        float Evaluate(T item);
    }
}