namespace Chinchillada
{
    using System;
    using UnityEngine;

    [Serializable]
    public class FrameCountSeed : IRandomSeedStrategy
    {
        public int GenerateSeed() => Time.frameCount;
    }
}