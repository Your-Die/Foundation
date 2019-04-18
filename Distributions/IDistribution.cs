namespace Chinchillada.Distributions
{
    public interface IDistribution<out T>
    {
        /// <summary>
        /// Sample the distribution.
        /// </summary>
        /// <returns>The sample.</returns>
        T Sample();
    }
}