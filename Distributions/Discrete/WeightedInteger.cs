namespace Chinchillada.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using SDU = StandardDiscreteUniform;

    public class WeightedInteger : IDiscreteDistribution<int>
    {
        private readonly IList<int> _weights;
        private readonly IDiscreteDistribution<int>[] _distributions;

        public static IDiscreteDistribution<int> Distribution(params int[] weights)
        {
            // Ensure we have valid weights.
            if (weights.Any(x => x < 0) || !weights.Any(x => x > 0))
            {
                throw new ArgumentException();
            }

            var trimmedWeights = weights.DropEndWhile(weight => weight == 0).ToArray();
            
            if (trimmedWeights.Length == 1)
            {
                return Singleton<int>.Distribution(0);
            }

            if (trimmedWeights.GetIndexIfSingle(i => i != 0, out int index))
            {
                return Singleton<int>.Distribution(index);
            }

            if (trimmedWeights.Length == 2)
            {
                int zeroes = weights[0];
                int ones = weights[1];
                return Bernoulli.Distribution(zeroes, ones);
            }

            return new WeightedInteger(trimmedWeights);
        }

        private WeightedInteger(IEnumerable<int> weights)
        {
            _weights = weights.EnsureList();
            int sum = _weights.Sum();
            int count = _weights.Count;
            _distributions = new IDiscreteDistribution<int>[count];

            var lows = new Dictionary<int, int>();
            var highs = new Dictionary<int, int>();
            for (int i = 0; i < count; i++)
            {
                int weight = _weights[i] * count;
                if (weight == sum)
                    _distributions[i] = Singleton<int>.Distribution(i);
                else if (weight < sum)
                    lows.Add(i, weight);
                else
                    highs.Add(i, weight);
            }

            while (lows.Any())
            {
                var low = lows.First();
                lows.Remove(low.Key);
                var high = highs.First();
                highs.Remove(high.Key);

                int lowNeeds = sum - low.Value;
                _distributions[low.Key] =
                    Bernoulli.Distribution(low.Value, lowNeeds)
                             .Select(i => i == 0 ? low.Key : high.Key);

                int newHigh = high.Value - lowNeeds;
                if (newHigh == sum)
                    _distributions[high.Key] = Singleton<int>.Distribution(high.Key);
                else if (newHigh < sum)
                    lows[high.Key] = newHigh;
                else
                    highs[high.Key] = newHigh;
            }
        }

        public int Sample()
        {
            int index = _weights.IndexDistribution().Sample();
            var distribution = _distributions[index];
            return distribution.Sample();
        }

        public IEnumerable<int> Support()
        {
            return Enumerable.Range(0, _weights.Count).Where(i => _weights[i] != 0);
        }

        public int Weight(int variable)
        {
            return _weights.ElementAtOrDefault(variable);
        }
    }
}
