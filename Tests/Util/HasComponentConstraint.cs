namespace Chinchillada.Tests
{
    using NUnit.Framework.Constraints;
    using UnityEngine;

    public static class HasComponentConstraintExtensions
    {
        public static HasComponentConstraint<T> Component<T>(this ConstraintExpression expression,
                                                                SearchStrategy strategy = SearchStrategy.FindComponent)
        {
            var constraint = new HasComponentConstraint<T>(strategy);

            expression.Append(constraint);

            return constraint;
        }
    }

    public class Has : NUnit.Framework.Has
    {
        public static HasComponentConstraint<T> Component<T>(SearchStrategy strategy = SearchStrategy.FindComponent)
        {
            return new HasComponentConstraint<T>(strategy);
        }
    }

    public class HasComponentConstraint<T> : Constraint
    {
        private readonly SearchStrategy searchStrategy;

        public HasComponentConstraint(SearchStrategy searchStrategy)
        {
            this.searchStrategy = searchStrategy;
        }

        public override ConstraintResult ApplyTo(object actual)
        {
            var result = this.SatisfiesConstraint(actual);
            return new ConstraintResult(this, actual, result);
        }

        private bool SatisfiesConstraint(object actual)
        {
            return TryGetGameObject(actual, out var gameObject) && this.HasComponent(gameObject);
        }

        private bool HasComponent(GameObject gameObject)
        {
            var component = this.searchStrategy.FindComponent<T>(gameObject);
            return component != null;
        }

        private static bool TryGetGameObject(object actual, out GameObject result)
        {
            switch (actual)
            {
                case GameObject gameObject:
                    result = gameObject;
                    return true;
                case Component component:
                    result = component.gameObject;
                    return true;
                default:
                    result = null;
                    return false;
            }
        }
    }
}