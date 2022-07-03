namespace Chinchillada
{
    public interface IHasDuration
    {
        float Duration { get; }
        
        float Remaining { get; }
    }

    public static class DurationExtensions
    {
        public static float Elapsed(this IHasDuration duration)
        {
            return duration.Duration - duration.Remaining;
        }
    }
}