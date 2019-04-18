namespace Chinchillada.Distributions
{
    using System.Linq;

    using SCU = StandardContinuousUniform;

    /// <summary>
    /// Distribution sampled by calculating the mean of <see cref="SampleCount"/> independent uniform variables. 
    /// </summary>
    public class Bates : IDistribution<float>
    {
        private Bates(int sampleCount)
        {
            this.SampleCount = sampleCount;
        }

        /// <summary>
        /// The amount of individual <see cref="SCU"/> samples averaged to generate a <see cref="Bates"/> sample.
        /// </summary>
        public int SampleCount { get; }

        public float Sample()
        {
            return SCU.Distribution.Samples().Take(this.SampleCount).Average();
        }
    }
}
