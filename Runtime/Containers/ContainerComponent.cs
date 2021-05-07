namespace Chinchillada
{
    using Sirenix.Serialization;

    public class ContainerComponent<T> : ChinchilladaBehaviour, IDelegateContainer<T>
    {
        [OdinSerialize] private IDelegateContainer<T> container;

        public T Get() => this.container.Get();

        public void Set(T value) => this.container.Set(value);
        public void Refresh() => this.container.Refresh();
    }
}