using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada
{
    public static class Entropy
    {
        public static float Shannon(IEnumerable<float> weights)
        {
            var sum = 0f;
            var loggedSum = 0f;

            foreach (var weight in weights)
            {
                var weightLog = Mathf.Log(weight);
                
                sum += weight;
                loggedSum += weight * weightLog;
            }

            return Mathf.Log(sum) - loggedSum / sum;
        }
    }
}