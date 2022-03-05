namespace Chinchillada.PCGraph
{
    public class URandomFactory : IFactory<UnityRandom>
    {
        public UnityRandom Create() => UnityRandom.Shared;
    }
}