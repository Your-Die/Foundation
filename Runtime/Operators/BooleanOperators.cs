namespace Chinchillada
{
    using System;

    public class And : IOperator<bool, bool>
    {
        public bool Execute(bool left, bool right) => left && right;
    }

    public class Or : IOperator<bool, bool>
    {
        public bool Execute(bool left, bool right) => left || right;
    }

    public class Equals<T> : IBooleanOperator<T> where T : IEquatable<T>
    {
        public bool Execute(T left, T right) => left.Equals(right);
    }

    public abstract class ComparisonOperator<T> : IBooleanOperator<T> where T : IComparable<T>
    {
        public bool Execute(T left, T right)
        {
            int comparisonResult = left.CompareTo(right);
            return this.EvaluateResult(comparisonResult);
        }

        protected abstract bool EvaluateResult(int comparisonResult);
    }


    public class GreaterThan<T> : ComparisonOperator<T> where T : IComparable<T>
    {
        protected override bool EvaluateResult(int comparisonResult)
        {
            return comparisonResult > 0;
        }
    }
    
    public class LesserThan<T> : ComparisonOperator<T>  where T : IComparable<T>
    {
        protected override bool EvaluateResult(int comparisonResult)
        {
            return comparisonResult < 0;
        }
    }

    public class GreaterOrEqual<T> : ComparisonOperator<T>  where T : IComparable<T>
    {
        protected override bool EvaluateResult(int comparisonResult)
        {
            return comparisonResult >= 0;
        }
    }

    public class LesserOrEqual<T> : ComparisonOperator<T>  where T : IComparable<T>
    {
        protected override bool EvaluateResult(int comparisonResult)
        {
            return comparisonResult <= 0;
        }
    }
}