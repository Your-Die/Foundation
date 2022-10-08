namespace Chinchillada
{
    using System;
    using Datastructures;
    using UnityEngine;

    [Serializable]
    public class FloatToIntMapper
    {
        [SerializeField] private Range    inputRange;
        [SerializeField] private RangeInt outputRange;

        public FloatToIntMapper(Range inputRange, RangeInt outputRange)
        {
            this.inputRange  = inputRange;
            this.outputRange = outputRange;
        }

        public int Map(float value)
        {
            var percentage = this.inputRange.InverseLerp(value);
            return (int)Mathf.Lerp(this.outputRange.start, this.outputRange.end, percentage);
        }
    }
}