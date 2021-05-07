using UnityEngine;

namespace Chinchillada
{
    public interface IAwaitable
    {
        bool KeepWaiting { get; }
    }

    public static class AWaitableExtensions
    {
        public static CustomYieldInstruction GetAwaiter(this IAwaitable awaitable)
        {
            return new CustomAwaiter(() => awaitable.KeepWaiting);
        }
    }
}