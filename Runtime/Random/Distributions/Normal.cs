namespace Chinchillada.Distributions
{
    using System;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    [Serializable]
    public class Normal : IDistribution<float>
    {
        [SerializeField] private float mean;

        [SerializeField] private float sigma;

        public static readonly Normal Standard = new Normal(0, 1);

        private Normal(float mean, float sigma)
        {
            this.mean  = mean;
            this.sigma = sigma;
        }
        
        public static IDistribution<float> Distribution(float mean, float sigma) => new Normal(mean, sigma);

        public float Sample(IRNG random)
        {
            var sample1 = SCU.Distribution.Sample(random);
            var sample2 = SCU.Distribution.Sample(random);

            return Mathf.Sqrt(-2.0f * Mathf.Log(sample1))
                 * Mathf.Cos(2.0f   * Mathf.PI * sample2);
        }
    }
}