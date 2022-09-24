namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Bernoulli : IDiscreteDistribution<bool>
    {
        [SerializeField] private int trueWeight;
        [SerializeField] private int falseWeight;

        private Bernoulli(int trueWeight, int falseWeight)
        {
            this.trueWeight  = trueWeight;
            this.falseWeight = falseWeight;
        }

        public static IDiscreteDistribution<bool> Distribution(int trueWeight, int falseWeight)
        {
            if (trueWeight < 0 || falseWeight < 0 || trueWeight == 0 && falseWeight == 0)
                throw new ArgumentException();

            if (trueWeight == 0)
                return Singleton<bool>.Distribution(false);
            if (falseWeight == 0)
                return Singleton<bool>.Distribution(true);

            return new Bernoulli(trueWeight, falseWeight);
        }

        public bool Sample(IRNG random)
        {
            var probability = (float)this.trueWeight / (this.trueWeight + this.falseWeight);
            return random.Flip(probability);
        }

        public IEnumerable<bool> Support()
        {
            yield return true;
            yield return false;
        }

        public int Weight(bool item)
        {
            return item ? this.trueWeight : this.falseWeight;
        }
    }
}