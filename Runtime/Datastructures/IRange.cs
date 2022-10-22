namespace Chinchillada
{
    public interface IRange<T>
    {
        T Minimum { get; set; }
        T Maximum { get; set; }

        T Size { get; }

        T Clamp(T value);

        T InverseLerp(T value);
    }
}