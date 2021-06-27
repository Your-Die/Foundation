namespace Chinchillada
{
    using Foundation;

    public class PoolOfPools : SingletonBehaviour<PoolOfPools>
    {
        protected override void Awake()
        {
            base.Awake();
            this.name = "[Object Pooling]";
        }
    }
}