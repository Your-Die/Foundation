using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Common
{
    public interface IBuilder<out T>
    {
        T Build();
    }

    public interface IIterativeBuilder<T>
    {
        IEnumerable<T> BuildIterative();
    }

    public static class BuilderExtensions
    {
        public static T Build<T>(this IIterativeBuilder<T> builder)
        {
            return builder.BuildIterative().Last();
        }
    }
}