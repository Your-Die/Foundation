namespace Chinchillada.Foundation
{
    public interface IUtilityExecutor<T>
    {
        UtilitySystem<T> UtilitySystem { get; }

        void ExecuteOption(T option);

        void Stop();
    }

    public static class PerformerMethods
    {
        public static void AddOption<T>(this IUtilityExecutor<T> utilityExecutor, object id, T option, int utility)
        {
            var utilityOption = new UtilityOption<T>(id, option, utility);
            utilityExecutor.UtilitySystem.AddOption(utilityOption);
        }

        public static void AddOption<T>(this IUtilityExecutor<T> utilityExecutor, T option, int utility)
        {
            utilityExecutor.AddOption(option, option, utility);
        }

        public static void RemoveOption<T>(this IUtilityExecutor<T> utilityExecutor, object context)
        {
            utilityExecutor.UtilitySystem.RemoveOption(context);
        }
    }
}