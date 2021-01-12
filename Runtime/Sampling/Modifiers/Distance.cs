namespace Chinchillada.Sampling
{
    using UnityEngine;

    public class Distance : ISampleModifier
    {
        [SerializeField] [Range(0, 1)] private float point;
        
        public float Process(float sample, float percentage)
        {
            return Mathf.Abs(this.point - sample);
        }
    }
}