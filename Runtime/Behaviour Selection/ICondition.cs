namespace Chinchillada.Behavior
{
    using System.Collections.Generic;
    using System.Linq;

    public interface ICondition
    {
        bool Validate();
    }

    public interface ICondition<in T>
    {
        bool Validate(T value);
    }

    public static class ConditionExtensions
    {
        public static bool ValidateAll(this IEnumerable<ICondition> conditions)
        {
            return conditions.All(condition => condition.Validate());
        }
    }
}