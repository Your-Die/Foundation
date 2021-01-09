using System;
using System.Collections.Generic;

namespace Chinchillada
{
    public interface IGenerator<T>
    {
        T Result { get; }
        event Action<T> Generated;
        T Generate();
    }
    public static class GeneratorExtensions
    {
        public static IEnumerable<T> Generate<T>(this IGenerator<T> generator, int amount)
        {
            for (var i = 0; i < amount; i++)
                yield return generator.Generate();
        }
    }}