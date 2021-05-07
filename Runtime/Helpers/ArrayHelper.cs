using System;

namespace Chinchillada
{
    public static class ArrayHelper
    {
        public static T[] Create<T>(int count, Func<int, T> elementCreator)
        {
            var array = new T[count];
            
            for (var index = 0; index < count; index++) 
                array[index] = elementCreator.Invoke(index);

            return array;
        }

        public static T[] Create<T>(int count, Func<T> elementCreator)
        {
            var array = new T[count];
            for (var index = 0; index < count; index++) 
                array[index] = elementCreator.Invoke();

            return array;    
        }
    }
}