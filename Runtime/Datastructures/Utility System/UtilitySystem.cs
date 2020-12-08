namespace Chinchillada.Foundation
{
    using System.Collections.Generic;
    using Foundation;

    public class UtilitySystem<T>
    {
        private readonly IUtilityExecutor<T> utilityExecutor;
        private readonly Dictionary<object, UtilityOption<T>> options = new Dictionary<object, UtilityOption<T>>();

        public LogHandler Logger { get; set; }

        private UtilityOption<T> CurrentOption { get; set; }

        private IEnumerable<UtilityOption<T>> Options => this.options.Values;

        public UtilitySystem(IUtilityExecutor<T> utilityExecutor) => this.utilityExecutor = utilityExecutor;

        public void AddOption(UtilityOption<T> option)
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
            var option = new UtilityOption<T>(id, content, utility);
            this.AddOption(option);
        }

        public void RemoveOption(UtilityOption<T> option)
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
            this.utilityExecutor.Stop();
        }

        private void OnOptionAdded(UtilityOption<T> option)
        {
            if (this.CurrentOption == null || this.CurrentOption.Utility < option.Utility)
                this.ExecuteOption(option);
        }

        private void OnOptionRemoved(UtilityOption<T> option)
        {
            if (option != this.CurrentOption)
                return;

            if (this.Options.IsEmpty())
            {
                this.CurrentOption = null;
                this.utilityExecutor.Stop();
            }
            else
            {
                var highestUtility = this.Options.ArgMax(audienceMember => audienceMember.Utility);
                this.ExecuteOption(highestUtility);
            }
        }

        private void ExecuteOption(UtilityOption<T> option)
        {
            this.CurrentOption = option;
            this.TriggerExecution();
        }

        public void TriggerExecution()
        {
            this.utilityExecutor.ExecuteOption(this.CurrentOption.Content);
            this.Logger?.Log(
                $"Request {this.CurrentOption.Content} by {this.CurrentOption} performed by {this.utilityExecutor}");
        }
    }
}