using System;

namespace Chinchillada.Utilities
{
    [Serializable]
    public abstract class VariableReference<T>
        where T : IComparable
    {
        protected abstract T ConstantValue { get; }

        protected abstract SharedVariable<T> Variable { get; }

        protected abstract bool UseConstant { get; }

        public T Value => UseConstant ? ConstantValue : Variable.Value;
    }
}