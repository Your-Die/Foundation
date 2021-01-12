namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class ValueMixer
    {
        [Range(0, 1)]
        [SerializeField]
        [OnValueChanged(nameof(UpdateWeightY))]
        private float weightX;

        [ReadOnly]
        [Range(0, 1)]
        [SerializeField]
        private float weightY;

        public float Mix(float x, float y) => this.weightX * x + this.weightY * y;

        private void UpdateWeightY()
        {
            this.weightY = 1 - this.weightX;
        }
    }
}