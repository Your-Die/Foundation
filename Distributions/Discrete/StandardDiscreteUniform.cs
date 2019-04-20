namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SCU = StandardContinuousUniform;
    using SDU = StandardDiscreteUniform;

    public sealed class StandardDiscreteUniform : IDiscreteDistribution<int>
    {
        private StandardDiscreteUniform(int minimum, int maximum)
        {
            if (minimum > maximum)
            {
                throw new ArgumentException();
            }

            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        public int Minimum { get; }
        public int Maximum { get; }

        public int Size => 1 + this.Maximum - this.Minimum;

        public static SDU Distribution(int minimum, int maximum) => new SDU(minimum, maximum);

        public int Sample()
        {
            float continuousSample = SCU.Distribution.Sample() * this.Size;
            return (int) (continuousSample + Minimum);
        }

        public IEnumerable<int> Support() => Enumerable.Range(this.Minimum, this.Size);

        public int Weight(int variable)
        {
            return this.Minimum <= variable && variable <= this.Maximum
                ? 1
                : 0;
        }

        public override string ToString()
        {
            return $"Standard Discrete Uniform[{Minimum}, {Maximum}]";
        }
    }
}
