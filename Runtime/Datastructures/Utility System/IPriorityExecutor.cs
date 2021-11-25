namespace Chinchillada
{
    public interface IPriorityExecutor<T>
    {
        void ExecuteOption(T option);

        void Stop();
    }
}