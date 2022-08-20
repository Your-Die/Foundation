namespace Chinchillada
{
    using System;

    public static class Attempt
    {
        public static bool To(int attempts, Func<bool> action)
        {
            for (var i = 0; i < attempts; i++)
            {
                if (action.Invoke())
                    return true;
            }

            return false;
        }
    }
}