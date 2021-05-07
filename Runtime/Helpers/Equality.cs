namespace Chinchillada
{
    public static class Equality
    {
        public static bool Null<T>(T value)
        {
            return value == null;
        }

        public static bool NotNull<T>(T value) 
        {
            return value != null;
        }
    }
}
