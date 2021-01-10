namespace Utilities.Common
{
    public interface IScorer<T>
    {
        float Evaluate(T item);
    }
}