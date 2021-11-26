namespace Chinchillada
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class EventCounter : EventListenerBase
    {
        [OdinSerialize, Required] private IVariable<int> count;

        protected override void OnEventHappened() => this.count.Value++;
    }
}