namespace Chinchillada
{
    public interface IPriorityExecutor<T>
    {
        PrioritySystem<T> PrioritySystem { get; }

        void ExecuteOption(T option);

        void Stop();
    }

    public static class PerformerMethods
    {
        public static void AddOption<T>(this IPriorityExecutor<T> priorityExecutor, object id, T option, int utility)
        {
            var utilityOption = new PriorityOption<T>(id, option, utility);
            priorityExecutor.PrioritySystem.AddOption(utilityOption);
        }

        public static void AddOption<T>(this IPriorityExecutor<T> priorityExecutor, T option, int utility)
        {
            priorityExecutor.AddOption(option, option, utility);
        }

        public static void RemoveOption<T>(this IPriorityExecutor<T> priorityExecutor, object context)
        {
            priorityExecutor.PrioritySystem.RemoveOption(context);
        }
    }
}