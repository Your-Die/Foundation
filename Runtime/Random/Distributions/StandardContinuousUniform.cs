namespace Chinchillada.Distributions
{
    using System;

    [Serializable]
    public class StandardContinuousUniform : IDistribution<float>
    {
        public static readonly StandardContinuousUniform Distribution = new StandardContinuousUniform();
        public float Sample(IRNG random) => random.Float();
    }
}