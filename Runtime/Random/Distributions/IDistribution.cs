namespace Chinchillada
{
    public interface IDistribution<out T>
    {
        public T Sample(IRNG random);
    }
}