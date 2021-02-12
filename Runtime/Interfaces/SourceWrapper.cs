namespace Interfaces
{
    using System;
    using Chinchillada.Foundation;
    using Sirenix.Serialization;

    [Serializable]
    public class SourceWrapper<T> : ISource<T>
    {
        [OdinSerialize] private T item;

        public SourceWrapper() => this.item = default;
        
        public SourceWrapper(T item) => this.item = item;

        public T Get() => this.item;
    }
}