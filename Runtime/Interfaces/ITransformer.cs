namespace Chinchillada.Generation
{
    public interface ITransformer<in TIn, out TOut>
    {
        TOut Transform(TIn input);
    }
}