using System;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Serializable reference to a <see cref="SharedVariable{T}"/>.
    /// Can also contain a constant value instead.
    /// </summary>
    [Serializable]
    public abstract class VariableReference<T>
        where T : IComparable
    {
        /// <summary>
        /// The constant value if we use that instead of a <see cref="SharedVariable{T}"/>.
        /// </summary>
        protected abstract T ConstantValue { get; }

        /// <summary>
        /// The <see cref="SharedVariable{T}"/> this <see cref="VariableReference{T}"/> is referencing.
        /// </summary>
        protected abstract SharedVariable<T> Variable { get; }

        /// <summary>
        /// Whether to use a <see cref="ConstantValue"/> instead of a <see cref="Variable"/>.
        /// </summary>
        protected abstract bool UseConstant { get; }

        /// <summary>
        /// The value.
        /// </summary>
        public T Value => UseConstant ? ConstantValue : Variable.Value;
    }
}