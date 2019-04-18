namespace Chinchillada.Distributions
{
    using UnityEngine;
    using SCU = StandardContinuousUniform;
    
    public class StandardContinuousUniform : IDistribution<float>
    {
        public static readonly SCU Distribution = new SCU();

        private StandardContinuousUniform()
        {
        }

        public float Sample()
        {
            return Random.value;
        }
    }
}
