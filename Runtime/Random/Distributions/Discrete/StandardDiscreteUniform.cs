namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SCU = StandardContinuousUniform;

    [Serializable]
    public class StandardDiscreteUniform : IDiscreteDistribution<int>
    {
        [SerializeField] private int min;
        [SerializeField] private int max;

        private StandardDiscreteUniform(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public static StandardDiscreteUniform Distribution(int min, int max)
        {
            if (max < min)
                throw new ArgumentException();

            return new StandardDiscreteUniform(min, max);
        }

        public int Sample(IRNG random)
        {
            return random.Range(this.min, this.max, inclusive: true);
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(this.min, 1 + this.max - this.min);
        }

        public int Weight(int value)
        {
            return this.WithinRange(value) ? 1 : 0;
        }

        private bool WithinRange(int value)
        {
            return this.min <= value && value <= this.max;
        }
    }
}