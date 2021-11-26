namespace Chinchillada
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class VariableComponent<T> : ChinchilladaBehaviour, IVariable<T>
    {
        [OdinSerialize, Required] private IVariable<T> variable;

        public T Value
        {
            get => this.variable.Value;
            set => this.variable.Value = value;
        }
    }
}