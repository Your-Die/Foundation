namespace Chinchillada
{
    public interface ICopyable<out T>
    {
        T Copy();
    }
}