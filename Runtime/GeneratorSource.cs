namespace DefaultNamespace
{
    using Chinchillada;
    using Sirenix.Serialization;

    public class GeneratorSource<T> : ISource<T>
    {
        [OdinSerialize] private IGenerator<T> generator;
        
        public T Get() => this.generator.Generate();
    }
}