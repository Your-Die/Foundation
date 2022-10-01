namespace Chinchillada
{
    public interface IOperator<TIn, TOut>
    {
        TOut Execute(TIn left, TIn right);
    }

    public interface IBooleanOperator<TIn> : IOperator<TIn, bool>
    {
    }
}