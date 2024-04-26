using System;

namespace Chinchillada
{
    using System.Collections.Generic;

    public interface IObservableGenerator<T> : IGenerator<T>
    {
        event Action<T> Generated;
    }
    
    public interface IGenerator<out T>
    {
        T Generate(IRNG random);
    }

    public static class GeneratorExtensions
    {
        public static IEnumerable<T> Generate<T>(this IGenerator<T> generator, int amount, IRNG random)
        {
            for (var i = 0; i < amount; i++)
                yield return generator.Generate(random);
        }
    }
}