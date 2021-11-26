namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class VariableComponent<T> : AutoRefBehaviour, IVariable<T>
    {
        [OdinSerialize, Required] private IVariable<T> variable;

        public T Value
        {
            get => this.variable.Value;
            set => this.variable.Value = value;
        }

        public event Action<T> ValueChanged;

        private void OnEnable() => this.variable.ValueChanged += this.OnVariableChanged;

        private void OnDisable() => this.variable.ValueChanged -= this.OnVariableChanged;

        private void OnVariableChanged(T value) => this.ValueChanged?.Invoke(value);
    }
}