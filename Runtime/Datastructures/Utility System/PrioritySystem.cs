namespace Chinchillada
{
    using System.Collections.Generic;
    using Chinchillada;

    public class PrioritySystem<T>
    {
        private readonly IPriorityExecutor<T> priorityExecutor;
        private readonly Dictionary<object, IPriorityOption<T>> options = new Dictionary<object, IPriorityOption<T>>();

        public LogHandler Logger { get; set; }

        private IPriorityOption<T> CurrentOption { get; set; }

        private IEnumerable<IPriorityOption<T>> Options => this.options.Values;

        public PrioritySystem(IPriorityExecutor<T> priorityExecutor) => this.priorityExecutor = priorityExecutor;

        public void AddOption(IPriorityOption<T> option)
        {
            if (option == null)
                return;

            if (this.options.ContainsKey(option.ID))
                this.RemoveOption(option.ID);

            this.options[option.ID] = option;
            this.OnOptionAdded(option);
        }

        public void AddOption(object id, T content, int utility)
        {
            var option = new PriorityOption<T>(id, content, utility);
            this.AddOption(option);
        }

        public void RemoveOption(IPriorityOption<T> option)
        {
            if (this.options.Remove(option.ID))
                this.OnOptionRemoved(option);
        }

        public void RemoveOption(object id)
        {
            if (this.options.TryGetValue(id, out var option))
                this.RemoveOption(option);
        }

        public void Clear()
        {
            this.options.Clear();
            this.CurrentOption = null;
            this.priorityExecutor.Stop();
        }

        private void OnOptionAdded(IPriorityOption<T> option)
        {
            if (this.CurrentOption == null || this.CurrentOption.Priority < option.Priority)
                this.ExecuteOption(option);
        }

        private void OnOptionRemoved(IPriorityOption<T> option)
        {
            if (option != this.CurrentOption)
                return;

            if (this.Options.IsEmpty())
            {
                this.CurrentOption = null;
                this.priorityExecutor.Stop();
            }
            else
            {
                var highestUtility = this.Options.ArgMax(audienceMember => audienceMember.Priority);
                this.ExecuteOption(highestUtility);
            }
        }

        private void ExecuteOption(IPriorityOption<T> option)
        {
            this.CurrentOption = option;
            this.TriggerExecution();
        }

        public void TriggerExecution()
        {
            this.priorityExecutor.ExecuteOption(this.CurrentOption.Content);
            this.Logger?.Log(
                $"Request {this.CurrentOption.Content} by {this.CurrentOption} performed by {this.priorityExecutor}");
        }
    }
}