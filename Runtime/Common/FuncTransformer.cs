namespace Chinchillada
{
    using System;
    using Generation;

    public class FuncTransformer<TIn, TOut> : ITransformer<TIn, TOut>
    {
        private readonly Func<TIn, TOut> projection;

        public FuncTransformer(Func<TIn,TOut> projection)
        {
            this.projection = projection;
        }

        public TOut Transform(TIn input) => this.projection.Invoke(input);
    }
}