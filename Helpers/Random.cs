using RNG = UnityEngine.Random;

namespace Chinchillada.Utilities
{
    public static class Random
    {
        public static float Value => RNG.value;
        public static float value => RNG.value;

        public static int Range(int max)
        {
            return Range(0, max);
        }

        public static int Range(int min, int max)
        {
            return RNG.Range(min, max);
        }

        public static float Range(float max)
        {
            return Range(0, max);
        }

        public static float Range(float min, float max)
        {
            return RNG.Range(min, max);
        }
    }
}
