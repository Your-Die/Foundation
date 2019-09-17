using System.Collections.Generic;

namespace Chinchillada.Utilities
{
    public interface IRegister<T>
    {
        IList<T> Observers { get; }
    }

    public static class Observable
    {
        public static void Register<T>(this IRegister<T> register, T observer)
        {
            register.Observers.Add(observer);
        }

        public static void Unregister<T>(this IRegister<T> register, T observer)
        {
            register.Observers.Remove(observer);
        }
    }
}