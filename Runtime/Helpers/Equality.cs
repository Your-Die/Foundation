namespace Chinchillada.Foundation
{
    using System;

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

        public static bool UnityNull(object item)
        {
            switch (item)
            {
                case null:                           return true;
                case UnityEngine.Object unityObject: return unityObject == null;
                default:                             return false;
            }
        }
    }
}