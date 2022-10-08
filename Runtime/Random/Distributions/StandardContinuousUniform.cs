namespace Chinchillada.Distributions
{
    using System;

    [Serializable]
    public class StandardContinuousUniform : IDistribution<float>
    {
        public static readonly StandardContinuousUniform Distribution = new StandardContinuousUniform();

        private StandardContinuousUniform()
        {
        }
        
        public float Sample(IRNG random) => random.Float();
    }
}