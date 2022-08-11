namespace Chinchillada
{
    public interface IPriorityExecutor<T>
    {
        void ExecuteOption(T option);

        void Stop();
    }
    
    public abstract class PriorityExecutor<T> : IPriorityExecutor<T>
    {
        private readonly T defaultOption;

        protected PriorityExecutor(T defaultOption) => this.defaultOption = defaultOption;

        public void ExecuteOption(T option) => this.Execute(option);

        public void Stop() => this.Execute(this.defaultOption);

        protected abstract void Execute(T option);
    }
}