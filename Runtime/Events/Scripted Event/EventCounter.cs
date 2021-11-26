namespace Chinchillada
{
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;

    public class EventCounter : SharedInt
    {
        [OdinSerialize, Required] private ScriptedEvent @event;

        private void OnEnable() => this.@event.Happened += this.OnEventHappened;

        private void OnDisable() => this.@event.Happened -= this.OnEventHappened;

        private void OnEventHappened() => this.Value++;
    }
}