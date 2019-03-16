using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Utilities
{
    public static class Generation
    {
        public static IEnumerable<T> Generate<T>(this Func<T> factoryMethod)
        {
            while (true)
            {
                yield return factoryMethod();
            }
        }

        public static IEnumerable<T> Generate<T>(this Func<T> factoryMethod, int amount)
        {
            return factoryMethod.Generate().Take(amount);
        }

        public static IEnumerable<T> GenerateValid<T>(this Func<T> factoryMethod, Predicate<T> validator)
        {
            return factoryMethod.Generate().Where(value => validator(value));
        }

        public static IEnumerable<T> GenerateValid<T>(this Func<T> factoryMethod, Predicate<T> validator, int amount)
        {
            return factoryMethod.GenerateValid(validator).Take(amount);
        }
    }
}
