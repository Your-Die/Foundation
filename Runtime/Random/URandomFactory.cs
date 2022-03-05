namespace Chinchillada
{
    public class URandomFactory : IFactory<UnityRandom>
    {
        public UnityRandom Create() => UnityRandom.Shared;
    }
}